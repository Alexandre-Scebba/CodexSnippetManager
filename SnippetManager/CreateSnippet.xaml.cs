using System;
using System.Windows;
using SnippetManager.Data;
using SnippetManager.Models;
using SnippetManager.ViewModels;

namespace SnippetManager
{
    /// <summary>
    /// Interaction logic for CreateSnippet.xaml
    /// </summary>
    public partial class CreateSnippet : Window
    {
        private readonly ApplicationDbContext _context;

        public CreateSnippet(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context), "Database context cannot be null.");

            //load dummy data
          //  DataContext = new MainViewModel();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TitleTextBox.Text?.Trim();

                string content = new System.Windows.Documents.TextRange(ContentRichTextBox.Document.ContentStart, ContentRichTextBox.Document.ContentEnd)
                    .Text?.Trim();

                if (string.IsNullOrWhiteSpace(title))
                {
                    MessageBox.Show("Title cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(content))
                {
                    MessageBox.Show("Content cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // save as comma-separated 
                //string selectedLanguages = SelectedLanguages != null ? string.Join(",", SelectedLanguages) : string.Empty;
                //string selectedTags = SelectedTags != null ? string.Join(",", SelectedTags) : string.Empty;


                Snippet newSnippet = new()
                {
                    Title = title,
                    Content = content,
                    //Language = selectedLanguages, // save as comma-separated 
                    //Tags = selectedTags,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Snippets.Add(newSnippet);
                _context.SaveChanges();

                MessageBox.Show("Snippet saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the snippet: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
