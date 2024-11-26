using System;
using System.Windows;
using SnippetManager.Models;
using SnippetManager.Data;
using SnippetManager.ViewModels;
using Microsoft.EntityFrameworkCore;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows.Controls;
using System.Linq;

namespace SnippetManager
{
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
            var selectedLanguages = _snippet.Language?.Split(new[] { ", " }, StringSplitOptions.None) ?? Array.Empty<string>();
            foreach (var language in selectedLanguages)
            {
                _viewModel.SelectedLanguages.Add(language);
            }

            // Set selected tags
            var selectedTags = _snippet.Tags?.Split(new[] { ", " }, StringSplitOptions.None) ?? Array.Empty<string>();
            foreach (var tag in selectedTags)
            {
                _viewModel.SelectedTags.Add(tag);
            }

            // Set syntax highlighting based on the first selected language
            if (_viewModel.SelectedLanguages.Count > 0)
            {
                SetSyntaxHighlighting(_viewModel.SelectedLanguages[0]);
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

                MessageBox.Show("Snippet updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Error updating snippet: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating snippet: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LanguagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (string addedItem in e.AddedItems)
            {
                _viewModel.ToggleLanguageSelection(addedItem);
            }

            foreach (string removedItem in e.RemovedItems)
            {
                _viewModel.ToggleLanguageSelection(removedItem);
            }

            if (LanguagesListBox.SelectedItems.Count > 0)
            {
                SetSyntaxHighlighting(LanguagesListBox.SelectedItems[0]?.ToString() ?? string.Empty);
            }
            _viewModel.OnPropertyChanged(nameof(MainViewModel.SelectedLanguagesText));
        }

        private void TagsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (string addedItem in e.AddedItems)
            {
                _viewModel.SelectedTags.Add(addedItem);
            }

            foreach (string removedItem in e.RemovedItems)
            {
                _viewModel.SelectedTags.Remove(removedItem);
            }

            _viewModel.OnPropertyChanged(nameof(MainViewModel.SelectedTagsText));
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
