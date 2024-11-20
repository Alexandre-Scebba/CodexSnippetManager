using System;
using System.Windows;
using SnippetManager.Data;
using SnippetManager.Models;

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
            _context = context;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string content = ContentTextBox.Text;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Title and content cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Snippet newSnippet = new()
            {
                Title = title,
                Content = content,
                CreatedAt = DateTime.Now
            };

            _context.Snippets.Add(newSnippet);
            _context.SaveChanges();

            MessageBox.Show("Snippet saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}