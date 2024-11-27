using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using SnippetManager.Models;
using SnippetManager.ViewModels;

namespace SnippetManager;

/// <summary>
///     Interaction logic for CreateSnippet.xaml
/// </summary>
public partial class CreateSnippet : Window
{
    private readonly codexDBContext _context;
    private readonly ObservableCollection<Snippet> _snippets;
    private readonly MainViewModel _viewModel;

    public CreateSnippet(codexDBContext context, ObservableCollection<Snippet> snippets)
    {
        InitializeComponent();
        _context = context ?? throw new ArgumentNullException(nameof(context), "Database context cannot be null.");
        _snippets = snippets ?? throw new ArgumentNullException(nameof(snippets), "Snippets collection cannot be null.");
        _viewModel = new MainViewModel();
        DataContext = _viewModel;
    }

    public event EventHandler<Snippet>? SnippetCreated;

    private void LanguagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (string addedItem in e.AddedItems) _viewModel.ToggleLanguageSelection(addedItem);

        foreach (string removedItem in e.RemovedItems) _viewModel.ToggleLanguageSelection(removedItem);

        if (LanguagesListBox.SelectedItems.Count > 0)
        {
            var selectedLanguage = LanguagesListBox.SelectedItems[0]?.ToString() ?? string.Empty;
            _viewModel.SelectedLanguage = selectedLanguage;
            SetSyntaxHighlighting(selectedLanguage);
        }

        _viewModel.OnPropertyChanged(nameof(MainViewModel.SelectedLanguagesText));
    }

    private void TagsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (string addedItem in e.AddedItems) _viewModel.ToggleTagSelection(addedItem);

        foreach (string removedItem in e.RemovedItems) _viewModel.ToggleTagSelection(removedItem);

        _viewModel.OnPropertyChanged(nameof(MainViewModel.SelectedTagsText));
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // get user inputs
            var title = TitleTextBox.Text?.Trim() ?? string.Empty;
            var content = ContentEditor.Text?.Trim() ?? string.Empty;
            var selectedLanguages = string.Join(", ", _viewModel.SelectedLanguages); // ListBox for languages
            var selectedTags = string.Join(", ", _viewModel.SelectedTags); // ListBox for tags

            // validate 
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Title and content cannot be empty.");
                return;
            }

            // find CategoryId for the selected language
            var firstSelectedLanguage = selectedLanguages.Split(new[] { ", " }, StringSplitOptions.None).First().Trim();
            var category = _context.Categories
                .FirstOrDefault(c => c.Name == firstSelectedLanguage && c.Type == "Language");
            if (category == null)
            {
                MessageBox.Show($"Error: Language '{selectedLanguages}' is not available.");
                return;
            }

            var userId = GetCurrentUserId();

            // create snippet
            var newSnippet = new Snippet
            {
                UserId = userId,
                Title = title,
                Content = content,
                Language = selectedLanguages,
                Tags = selectedTags,
                CategoryId = category.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // save to db
            _context.Snippets.Add(newSnippet);
            _context.SaveChanges();

            // Add to ObservableCollection to update the UI dynamically
            _snippets.Add(newSnippet);

            // Raise the event
            OnSnippetCreated(newSnippet);

            MessageBox.Show("Snippet saved successfully!");
            Close();
        }
        catch (DbUpdateException ex)
        {
            MessageBox.Show($"Error saving snippet: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Debug.WriteLine($"Error saving snippet: {ex.Message}");
            if (ex.InnerException != null) Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving snippet: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Debug.WriteLine($"Error saving snippet: {ex.Message}");
        }
    }

    private int GetCurrentUserId()
    {
        if (MainViewModel.CurrentUser == null) throw new InvalidOperationException("No user is currently logged in.");

        return MainViewModel.CurrentUser.UserId;
    }

    private void OnSnippetCreated(Snippet snippet)
    {
        SnippetCreated?.Invoke(this, snippet);
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void SetSyntaxHighlighting(string language)
    {
        ContentEditor.SyntaxHighlighting = SyntaxHighLightingHelper.GetSyntaxHighlighting(language);
    }
}
