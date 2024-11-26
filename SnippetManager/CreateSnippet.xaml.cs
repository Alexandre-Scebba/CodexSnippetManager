using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using SnippetManager.Data;
using SnippetManager.Models;
using SnippetManager.ViewModels;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ICSharpCode.AvalonEdit.Highlighting;

namespace SnippetManager
{
    /// <summary>
    /// Interaction logic for CreateSnippet.xaml
    /// </summary>
    public partial class CreateSnippet : Window
    {
        private readonly codexDBContext _context;
        private readonly ObservableCollection<Snippet> _snippets;
        private readonly MainViewModel _viewModel;

        public event EventHandler<Snippet>? SnippetCreated;

        public CreateSnippet(codexDBContext context, ObservableCollection<Snippet> snippets)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context), "Database context cannot be null.");
            _snippets = snippets ?? throw new ArgumentNullException(nameof(snippets), "Snippets collection cannot be null.");
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void LanguagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (string addedItem in e.AddedItems)
            {
                if (!_viewModel.SelectedLanguages.Contains(addedItem))
                {
                    _viewModel.SelectedLanguages.Add(addedItem);
                }
            }

            foreach (string removedItem in e.RemovedItems)
            {
                _viewModel.SelectedLanguages.Remove(removedItem);
            }

            if (LanguagesListBox.SelectedItems.Count > 0)
            {
                SetSyntaxHighlighting(LanguagesListBox.SelectedItems[0]?.ToString() ?? string.Empty);
            }
            _viewModel.OnPropertyChanged(nameof(MainViewModel.SelectedLanguagesText));
        }

        private void TagsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                foreach (string added in e.AddedItems)
                {
                    if (!viewModel.SelectedTags.Contains(added))
                    {
                        viewModel.SelectedTags.Add(added);
                    }
                }

                foreach (string removed in e.RemovedItems)
                {
                    if (viewModel.SelectedTags.Contains(removed))
                    {
                        viewModel.SelectedTags.Remove(removed);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // get user inputs
                string title = TitleTextBox.Text?.Trim() ?? string.Empty;
                string content = ContentEditor.Text?.Trim() ?? string.Empty;
                string selectedLanguages = string.Join(", ", LanguagesListBox.SelectedItems.Cast<string>()); // ListBox for languages
                string selectedTags = string.Join(", ", TagsListBox.SelectedItems.Cast<string>()); // ListBox for tags

                // validate 
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                {
                    MessageBox.Show("Title and content cannot be empty.");
                    return;
                }

                // find CategoryId for the selected language
                var firstSelectedLanguage = selectedLanguages.Split(',').First().Trim();
                var category = _context.Categories
                    .FirstOrDefault(c => c.Name == firstSelectedLanguage && c.Type == "Language");
                if (category == null)
                {
                    MessageBox.Show($"Error: Language '{selectedLanguages}' is not available.");
                    return;
                }

                int userId = GetCurrentUserId();

                // create snippet
                Snippet newSnippet = new Snippet
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
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Error saving snippet: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine($"Error saving snippet: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving snippet: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine($"Error saving snippet: {ex.Message}");
            }
        }

        private int GetCurrentUserId()
        {
            if (MainViewModel.CurrentUser == null)
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }

            return MainViewModel.CurrentUser.UserId;
        }

        private void OnSnippetCreated(Snippet snippet)
        {
            SnippetCreated?.Invoke(this, snippet);
        }

        private void SetSyntaxHighlighting(string language)
        {
            switch (language.ToLower())
            {
                case "c#":
                case "csharp":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
                    break;
                case "xml":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("XML");
                    break;
                case "html":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("HTML");
                    break;
                case "javascript":
                case "js":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("JavaScript");
                    break;
                case "python":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("Python");
                    break;
                case "sql":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("SQL");
                    break;
                case "java":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("Java");
                    break;
                case "css":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("CSS");
                    break;
                case "php":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("PHP");
                    break;
                case "ruby":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("Ruby");
                    break;
                case "json":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("JSON");
                    break;
                case "typescript":
                case "ts":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("TypeScript");
                    break;
                case "markdown":
                case "md":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("Markdown");
                    break;
                case "vb":
                case "visualbasic":
                    ContentEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("VBNET");
                    break;
                default:
                    ContentEditor.SyntaxHighlighting = null;
                    break;
            }
        }
    }
}
