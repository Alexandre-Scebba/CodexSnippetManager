using SnippetManager.Data;
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
        public Dashboard()
        {
            InitializeComponent();
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the tag input from the TagInputTextBox
            string newTag = TagInputTextBox.Text;

            // Add the new tag to the TagsListBox if it is not empty
            if (!string.IsNullOrWhiteSpace(newTag))
            {
                TagsListBox.Items.Add(newTag);
                TagInputTextBox.Clear(); // Clear the input after adding
            }
        }


        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the language input from the LanguageInputTextBox
            string newLanguage = LanguageInputTextBox.Text;

            // Add the new language to the LanguagesListBox if it is not empty
            if (!string.IsNullOrWhiteSpace(newLanguage))
            {
                LanguagesListBox.Items.Add(newLanguage);
                LanguageInputTextBox.Clear(); // Clear the input after adding
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
