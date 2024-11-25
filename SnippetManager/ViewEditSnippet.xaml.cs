using System;
using System.Windows;
using SnippetManager.Models;
using SnippetManager.Data;
using Microsoft.EntityFrameworkCore;

namespace SnippetManager
{
    public partial class ViewEditSnippet : Window
    {
        private readonly codexDBContext _context;
        private readonly Snippet _snippet;

        public ViewEditSnippet(codexDBContext context, Snippet snippet)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context), "Database context cannot be null.");
            _snippet = snippet ?? throw new ArgumentNullException(nameof(snippet), "Snippet cannot be null.");

            // Load snippet data into text boxes
            TitleTextBox.Text = _snippet.Title;
            DescriptionTextBox.Text = _snippet.Description;
            LanguageTextBox.Text = _snippet.Language;
            TagsTextBox.Text = _snippet.Tags;
            ContentTextBox.Text = _snippet.Content;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Update snippet with new values
                _snippet.Title = TitleTextBox.Text.Trim();
                _snippet.Description = DescriptionTextBox.Text.Trim();
                _snippet.Language = LanguageTextBox.Text.Trim();
                _snippet.Tags = TagsTextBox.Text.Trim();
                _snippet.Content = ContentTextBox.Text.Trim();
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
    }
}
