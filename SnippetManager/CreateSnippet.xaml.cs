using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using SnippetManager.Data;
using SnippetManager.Models;
using SnippetManager.ViewModels;
using System.Globalization;
using System.Windows.Data;
using System.Diagnostics;

namespace SnippetManager
{
    /// <summary>
    /// Interaction logic for CreateSnippet.xaml
    /// </summary>
    public partial class CreateSnippet : Window
    {
        private readonly codexDBContext _context;

        public CreateSnippet(codexDBContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context), "Database context cannot be null.");

            //add
            DataContext = new MainViewModel();

        }

        //added
        private void LanguagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                foreach (string added in e.AddedItems)
                {
                    if (!viewModel.AvailableLanguages.Contains(added))
                    {
                        viewModel.AvailableLanguages.Add(added);
                    }
                }

                foreach (string removed in e.RemovedItems)
                {
                    if (viewModel.AvailableLanguages.Contains(removed))
                    {
                        viewModel.AvailableLanguages.Remove(removed);
                    }
                }
            }
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
                string availableLanguages = LanguageDropdown.SelectedItem?.ToString() ?? string.Empty;
                ; // Dropdown for language
                string selectedTags = string.Join(",", TagsListBox.SelectedItems.Cast<string>()); // ListBox for tags

                // validate 
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                {
                    MessageBox.Show("Title and content cannot be empty.");
                    return;
                }

                // find CategoryId for the selected language
                var category = _context.Categories
                    .FirstOrDefault(c => c.Name == availableLanguages && c.Type == "Language");
                if (category == null)
                {
                    MessageBox.Show($"Error: Language '{availableLanguages}' is not available.");
                    return;
                }

                int userId = GetCurrentUserId();

                // create snippet
                Snippet newSnippet = new Snippet
                {
                    UserId = userId,
                    Title = title,
                    Content = content,
                    Language = availableLanguages,
                    Tags = selectedTags,
                    CategoryId = category.CategoryId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                // save to db
                _context.Snippets.Add(newSnippet);
                _context.SaveChanges();

                MessageBox.Show("Snippet saved successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving snippet: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
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
    }
}

