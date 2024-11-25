using Microsoft.EntityFrameworkCore;
using SnippetManager.Data;
using SnippetManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using SnippetManager.ViewModels;
using System.Collections.ObjectModel;
using SnippetManager.Dtos;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using System.Diagnostics;
using System.Windows.Input;

namespace SnippetManager
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private readonly codexDBContext _context;

        public Dashboard()
        {
            InitializeComponent();
            Debug.WriteLine("Dashboard initialized.");

            var optionsBuilder = new DbContextOptionsBuilder<codexDBContext>();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
            _context = new codexDBContext(optionsBuilder.Options);
            Debug.WriteLine("Database context initialized.");

            Snippets = new ObservableCollection<SnippetViewModel>();
            LoadCategories();
            LoadSnippets();
        }

        private void LoadCategories()
        {
            Debug.WriteLine("Loading categories...");
            var languages = _context.Categories.Where(c => c.Type == "Language").ToList();
            foreach (var language in languages)
            {
                LanguagesListBox.Items.Add(language.Name);
                Debug.WriteLine($"Loaded language: {language.Name}");
            }

            var tags = _context.Categories.Where(c => c.Type == "Tag").ToList();
            foreach (var tag in tags)
            {
                TagsListBox.Items.Add(tag.Name);
                Debug.WriteLine($"Loaded tag: {tag.Name}");
            }
        }

        private ObservableCollection<SnippetViewModel> _snippets;
        public ObservableCollection<SnippetViewModel> Snippets
        {
            get => _snippets;
            set
            {
                _snippets = value;
                SnippetsDataGrid.ItemsSource = _snippets;
            }
        }

        private void LoadSnippets()
        {
            Debug.WriteLine("Loading snippets...");
            if (MainViewModel.CurrentUser != null)
            {
                int currentUserId = MainViewModel.CurrentUser.UserId;

                var snippetsFromDb = _context.Snippets
                                                .Where(s => s.UserId == currentUserId)
                                                .ToList();

                var snippetViewModels = snippetsFromDb.Select(s => new SnippetViewModel { Snippet = s }).ToList();
                Snippets = new ObservableCollection<SnippetViewModel>(snippetViewModels);
                Debug.WriteLine($"Loaded {Snippets.Count} snippets for user ID {currentUserId}.");
            }
            else
            {
                MessageBox.Show("No user is currently logged in");
                Debug.WriteLine("No user is currently logged in.");
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();

            if (MainViewModel.CurrentUser != null)
            {
                int currentUserId = MainViewModel.CurrentUser.UserId;

                var filteredSnippets = Snippets
                    .Where(s => s.Snippet.UserId == currentUserId &&
                                ((s.Snippet.Title?.ToLower().Contains(searchTerm) ?? false) ||
                                 (s.Snippet.Description?.ToLower().Contains(searchTerm) ?? false) ||
                                 (s.Snippet.Tags?.ToLower().Contains(searchTerm) ?? false)))
                    .ToList();

                SnippetsDataGrid.ItemsSource = new ObservableCollection<SnippetViewModel>(filteredSnippets);
                Debug.WriteLine($"Filtered {filteredSnippets.Count} snippets for search term '{searchTerm}'.");
            }
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            string newTag = TagInputTextBox.Text;
            Debug.WriteLine($"Adding new tag: {newTag}");

            if (!string.IsNullOrWhiteSpace(newTag))
            {
                TagsListBox.Items.Add(newTag);
                TagInputTextBox.Clear();
                Debug.WriteLine($"Added tag to list: {newTag}");

                var category = new Category { Name = newTag, Type = "Tag" };
                _context.Categories.Add(category);
                _context.SaveChanges();
                Debug.WriteLine($"Saved new tag to database: {newTag}");
            }
        }

        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            string newLanguage = LanguageInputTextBox.Text;
            Debug.WriteLine($"Adding new language: {newLanguage}");

            if (!string.IsNullOrWhiteSpace(newLanguage))
            {
                LanguagesListBox.Items.Add(newLanguage);
                LanguageInputTextBox.Clear();
                Debug.WriteLine($"Added language to list: {newLanguage}");

                var category = new Category { Name = newLanguage, Type = "Language" };
                _context.Categories.Add(category);
                _context.SaveChanges();
                Debug.WriteLine($"Saved new language to database: {newLanguage}");
            }
        }

        private void NewSnippetButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Creating new snippet...");
            var optionsBuilder = new DbContextOptionsBuilder<codexDBContext>();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);

            var snippetsCollection = new ObservableCollection<Snippet>(_context.Snippets.ToList());
            CreateSnippet createSnippetWindow = new CreateSnippet(new codexDBContext(optionsBuilder.Options), snippetsCollection);

            createSnippetWindow.SnippetCreated += CreateSnippetWindow_SnippetCreated;

            createSnippetWindow.Show();
            Debug.WriteLine("New snippet window shown.");
        }

        private void CreateSnippetWindow_SnippetCreated(object sender, Snippet e)
        {
            Snippets.Add(new SnippetViewModel { Snippet = e });
            CommitAndRefreshDataGrid();
            Debug.WriteLine($"New snippet added: {e.Title}");
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Deleting selected snippets...");

            var selectedSnippets = Snippets.Where(s => s.IsSelected).ToList();

            if (selectedSnippets.Any())
            {
                foreach (var snippetViewModel in selectedSnippets)
                {
                    _context.Snippets.Remove(snippetViewModel.Snippet);
                    Snippets.Remove(snippetViewModel);
                    Debug.WriteLine($"Deleted snippet: {snippetViewModel.Snippet.Title}");
                }

                _context.SaveChanges();
                CommitAndRefreshDataGrid();
                MessageBox.Show("Selected snippets deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("Selected snippets deleted and changes saved to database.");
            }
            else
            {
                MessageBox.Show("No snippets selected for deletion.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Debug.WriteLine("No snippets selected for deletion.");
            }
        }

        private void NewSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Opening settings...");
            Settings createSettingsWindow = new Settings();

            createSettingsWindow.Show();
            Debug.WriteLine("Settings window shown.");
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Importing snippets...");
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                Debug.WriteLine($"Selected file for import: {filePath}");
                string json = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };
                var snippetDtos = JsonSerializer.Deserialize<List<SnippetDto>>(json, options);

                if (snippetDtos != null)
                {
                    foreach (var snippetDto in snippetDtos)
                    {
                        var newSnippet = new Snippet
                        {
                            UserId = MainViewModel.CurrentUser.UserId,
                            Title = snippetDto.Title,
                            Description = snippetDto.Description,
                            Language = snippetDto.Language,
                            Tags = snippetDto.Tags,
                            Content = snippetDto.Content,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        _context.Snippets.Add(newSnippet);
                        Debug.WriteLine($"Added new snippet: {newSnippet.Title}");
                    }
                    _context.SaveChanges();
                    LoadSnippets();
                    MessageBox.Show("Snippets imported successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Debug.WriteLine("Snippets imported and saved to database.");
                }
                else
                {
                    MessageBox.Show("No snippets found in the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Debug.WriteLine("No snippets found in the file.");
                }
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Exporting snippets...");
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                Debug.WriteLine($"Selected file for export: {filePath}");
                var selectedSnippets = Snippets
                    .Where(s => s.IsSelected)
                    .Select(s => new SnippetDto
                    {
                        Title = s.Snippet.Title,
                        Description = s.Snippet.Description,
                        Language = s.Snippet.Language,
                        Tags = s.Snippet.Tags,
                        Content = s.Snippet.Content
                    })
                    .ToList();
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(selectedSnippets, options);
                File.WriteAllText(filePath, json);
                MessageBox.Show("Selected snippets exported successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Debug.WriteLine("Selected snippets exported to file.");
            }
        }

        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox selectAllCheckBox)
            {
                bool isChecked = selectAllCheckBox.IsChecked ?? false;
                Debug.WriteLine($"Select All checkbox changed to: {isChecked}");
                foreach (var snippet in SnippetsDataGrid.Items)
                {
                    if (snippet is SnippetViewModel snippetItem)
                    {
                        snippetItem.IsSelected = isChecked;
                    }
                }
                CommitAndRefreshDataGrid();
            }
        }

        private void IndividualCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is SnippetViewModel snippetViewModel)
            {
                snippetViewModel.IsSelected = checkBox.IsChecked ?? false;
                Debug.WriteLine($"Snippet '{snippetViewModel.Snippet.Title}' selection changed to: {snippetViewModel.IsSelected}");
            }
        }

        private void SnippetsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SnippetsDataGrid.SelectedItem is SnippetViewModel selectedSnippetViewModel)
            {
                var optionsBuilder = new DbContextOptionsBuilder<codexDBContext>();
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);

                ViewEditSnippet viewEditSnippetWindow = new ViewEditSnippet(new codexDBContext(optionsBuilder.Options), selectedSnippetViewModel.Snippet);
                viewEditSnippetWindow.ShowDialog();

                CommitAndRefreshDataGrid();
            }
        }

        private void CommitAndRefreshDataGrid()
        {
            SnippetsDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            SnippetsDataGrid.Items.Refresh();
        }
    }
}
