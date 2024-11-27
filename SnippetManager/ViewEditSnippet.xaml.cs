using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using SnippetManager.Models;
using SnippetManager.ViewModels;

namespace SnippetManager;

public partial class ViewEditSnippet : Window
{
    private readonly codexDBContext _context;
    private readonly Snippet _snippet;
    private readonly MainViewModel _viewModel;

    public ViewEditSnippet(codexDBContext context, Snippet snippet)
    {
        InitializeComponent();
        _context = context ?? throw new ArgumentNullException(nameof(context), "Database context cannot be null.");
        _snippet = snippet ?? throw new ArgumentNullException(nameof(snippet), "Snippet cannot be null.");

        // Set DataContext to MainViewModel
        _viewModel = new MainViewModel();
        DataContext = _viewModel;

        // Load snippet data into controls
        TitleTextBox.Text = _snippet.Title;
        DescriptionTextBox.Text = _snippet.Description;
        ContentEditor.Text = _snippet.Content;

        // Set selected languages
        var selectedLanguages = _snippet.Language?.Split(new[] { ", " }, StringSplitOptions.None) ??
                                Array.Empty<string>();
        foreach (var language in selectedLanguages) _viewModel.SelectedLanguages.Add(language);

        // Set selected tags
        var selectedTags = _snippet.Tags?.Split(new[] { ", " }, StringSplitOptions.None) ?? Array.Empty<string>();
        foreach (var tag in selectedTags) _viewModel.SelectedTags.Add(tag);

        // Set syntax highlighting based on the first selected language
        if (_viewModel.SelectedLanguages.Count > 0)
        {
            var firstSelectedLanguage = _viewModel.SelectedLanguages[0];
            _viewModel.SelectedLanguage = firstSelectedLanguage;
            SetSyntaxHighlighting(firstSelectedLanguage);
        }
    }


    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Update snippet with new values
            _snippet.Title = TitleTextBox.Text.Trim();
            _snippet.Description = DescriptionTextBox.Text.Trim();
            _snippet.Language = string.Join(", ", _viewModel.SelectedLanguages);
            _snippet.Tags = string.Join(", ", _viewModel.SelectedTags);
            _snippet.Content = ContentEditor.Text.Trim();
            _snippet.UpdatedAt = DateTime.Now;

            // Save changes to database
            _context.Snippets.Update(_snippet);
            _context.SaveChanges();

            MessageBox.Show("Snippet updated successfully!", "Success", MessageBoxButton.OK,
                MessageBoxImage.Information);
            Close();
        }
        catch (DbUpdateException ex)
        {
            MessageBox.Show($"Error updating snippet: {ex.Message}", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            if (ex.InnerException != null) Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating snippet: {ex.Message}", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        TitleTextBox.Text = _snippet.Title;
        DescriptionTextBox.Text = _snippet.Description;
        ContentEditor.Text = _snippet.Content;

        _viewModel.SelectedLanguages.Clear();
        var selectedLanguages =
            _snippet.Language?.Split(new[] { ", " }, StringSplitOptions.None) ?? Array.Empty<string>();
        foreach (var language in selectedLanguages) _viewModel.SelectedLanguages.Add(language);

        _viewModel.SelectedTags.Clear();
        var selectedTags = _snippet.Tags?.Split(new[] { ", " }, StringSplitOptions.None) ?? Array.Empty<string>();
        foreach (var tag in selectedTags) _viewModel.SelectedTags.Add(tag);

        Close();
    }

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

    private void SetSyntaxHighlighting(string language)
    {
        ContentEditor.SyntaxHighlighting = SyntaxHighLightingHelper.GetSyntaxHighlighting(language);
    }
}