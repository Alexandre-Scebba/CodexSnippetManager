using SnippetManager.Data;
using SnippetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnippetManager
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private readonly ApplicationDbContext _context;

        public Dashboard()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadCategories();
        }

        private void LoadCategories()
        {
            // Load languages 
            var languages = _context.Categories.Where(c => c.Type == "Language").ToList();
            foreach (var language in languages)
            {
                LanguagesListBox.Items.Add(language.Name);
            }

            // Loadd tags
            var tags = _context.Categories.Where(c => c.Type == "Tag").ToList();
            foreach (var tag in tags)
            {
                TagsListBox.Items.Add(tag.Name);
            }
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            string newTag = TagInputTextBox.Text;

            // Add the new tag to the TagsListBox if it is not empty
            if (!string.IsNullOrWhiteSpace(newTag))
            {
                TagsListBox.Items.Add(newTag);
                TagInputTextBox.Clear();

                // Save the tag to the Categories table
                var category = new Category { Name = newTag, Type = "Tag" };
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
        }

        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            string newLanguage = LanguageInputTextBox.Text;

            // Add the new language to the LanguagesListBox if it is not empty
            if (!string.IsNullOrWhiteSpace(newLanguage))
            {
                LanguagesListBox.Items.Add(newLanguage);
                LanguageInputTextBox.Clear();

                // Save the language to the Categories table
                var category = new Category { Name = newLanguage, Type = "Language" };
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
        }

        private void NewSnippetButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of CreateSnippet
            CreateSnippet createSnippetWindow = new CreateSnippet(new ApplicationDbContext());

            // Show the window
            createSnippetWindow.Show();
        }
    }
}
