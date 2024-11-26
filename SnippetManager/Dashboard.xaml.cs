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
using ICSharpCode.AvalonEdit.Highlighting;
using System.Globalization;
using System.Windows.Data;

namespace SnippetManager
{
    public class LanguageToSyntaxHighlightingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string language)
            {
                switch (language.ToLower())
                {
                    case "c#":
                    case "csharp":
                        return HighlightingManager.Instance.GetDefinition("C#");
                    case "xml":
                        return HighlightingManager.Instance.GetDefinition("XML");
                    case "html":
                        return HighlightingManager.Instance.GetDefinition("HTML");
                    case "javascript":
                    case "js":
                        return HighlightingManager.Instance.GetDefinition("JavaScript");
                    case "typescript":
                    case "ts":
                        return HighlightingManager.Instance.GetDefinition("TypeScript");
                    case "python":
                        return HighlightingManager.Instance.GetDefinition("Python");
                    case "sql":
                        return HighlightingManager.Instance.GetDefinition("SQL");
                    case "java":
                        return HighlightingManager.Instance.GetDefinition("Java");
                    case "css":
                        return HighlightingManager.Instance.GetDefinition("CSS");
                    case "php":
                        return HighlightingManager.Instance.GetDefinition("PHP");
                    case "ruby":
                        return HighlightingManager.Instance.GetDefinition("Ruby");
                    case "json":
                        return HighlightingManager.Instance.GetDefinition("JSON");
                    case "markdown":
                    case "md":
                        return HighlightingManager.Instance.GetDefinition("Markdown");
                    case "vb":
                    case "visualbasic":
                        return HighlightingManager.Instance.GetDefinition("VBNET");
                    case "c++":
                    case "cpp":
                    case "cplusplus":
                        return HighlightingManager.Instance.GetDefinition("C++");
                    case "c":
                        return HighlightingManager.Instance.GetDefinition("C");
                    case "go":
                    case "golang":
                        return HighlightingManager.Instance.GetDefinition("Go");
                    case "swift":
                        return HighlightingManager.Instance.GetDefinition("Swift");
                    case "kotlin":
                        return HighlightingManager.Instance.GetDefinition("Kotlin");
                    case "r":
                        return HighlightingManager.Instance.GetDefinition("R");
                    case "perl":
                        return HighlightingManager.Instance.GetDefinition("Perl");
                    case "shell":
                    case "bash":
                    case "sh":
                        return HighlightingManager.Instance.GetDefinition("Bash");
                    case "powershell":
                    case "ps":
                        return HighlightingManager.Instance.GetDefinition("PowerShell");
                    case "yaml":
                    case "yml":
                        return HighlightingManager.Instance.GetDefinition("YAML");
                    case "dockerfile":
                    case "docker":
                        return HighlightingManager.Instance.GetDefinition("Dockerfile");
                    case "haskell":
                        return HighlightingManager.Instance.GetDefinition("Haskell");
                    case "lua":
                        return HighlightingManager.Instance.GetDefinition("Lua");
                    case "scala":
                        return HighlightingManager.Instance.GetDefinition("Scala");
                    case "rust":
                        return HighlightingManager.Instance.GetDefinition("Rust");
                    case "objective-c":
                    case "objc":
                    case "objectivec":
                        return HighlightingManager.Instance.GetDefinition("Objective-C");
                    case "groovy":
                        return HighlightingManager.Instance.GetDefinition("Groovy");
                    case "dart":
                        return HighlightingManager.Instance.GetDefinition("Dart");
                    case "sqlpl":
                    case "plsql":
                        return HighlightingManager.Instance.GetDefinition("PL/SQL");
                    case "matlab":
                        return HighlightingManager.Instance.GetDefinition("MATLAB");
                    case "fsharp":
                    case "f#":
                        return HighlightingManager.Instance.GetDefinition("F#");
                    case "lisp":
                    case "commonlisp":
                        return HighlightingManager.Instance.GetDefinition("Lisp");
                    case "clojure":
                        return HighlightingManager.Instance.GetDefinition("Clojure");
                    case "vbscript":
                    case "vbs":
                        return HighlightingManager.Instance.GetDefinition("VBScript");
                    case "sass":
                    case "scss":
                        return HighlightingManager.Instance.GetDefinition("Sass");
                    case "coffeescript":
                    case "coffee":
                        return HighlightingManager.Instance.GetDefinition("CoffeeScript");
                    case "erlang":
                        return HighlightingManager.Instance.GetDefinition("Erlang");
                    case "elixir":
                        return HighlightingManager.Instance.GetDefinition("Elixir");
                    case "fortran":
                        return HighlightingManager.Instance.GetDefinition("Fortran");
                    case "ada":
                        return HighlightingManager.Instance.GetDefinition("Ada");
                    case "verilog":
                        return HighlightingManager.Instance.GetDefinition("Verilog");
                    case "vhdl":
                        return HighlightingManager.Instance.GetDefinition("VHDL");
                    case "protobuf":
                    case "proto":
                        return HighlightingManager.Instance.GetDefinition("Protocol Buffers");
                    case "solidity":
                        return HighlightingManager.Instance.GetDefinition("Solidity");
                    case "tcl":
                        return HighlightingManager.Instance.GetDefinition("Tcl");
                    case "awk":
                        return HighlightingManager.Instance.GetDefinition("AWK");
                    case "nim":
                        return HighlightingManager.Instance.GetDefinition("Nim");
                    case "pascal":
                        return HighlightingManager.Instance.GetDefinition("Pascal");
                    case "haxe":
                        return HighlightingManager.Instance.GetDefinition("Haxe");
                    case "ocaml":
                        return HighlightingManager.Instance.GetDefinition("OCaml");
                    case "racket":
                        return HighlightingManager.Instance.GetDefinition("Racket");
                    case "crystal":
                        return HighlightingManager.Instance.GetDefinition("Crystal");
                    case "rexx":
                        return HighlightingManager.Instance.GetDefinition("REXX");
                    case "smalltalk":
                        return HighlightingManager.Instance.GetDefinition("Smalltalk");
                    case "vbnet":
                        return HighlightingManager.Instance.GetDefinition("VB.NET");
                    case "actionscript":
                    case "as":
                        return HighlightingManager.Instance.GetDefinition("ActionScript");
                    case "delphi":
                        return HighlightingManager.Instance.GetDefinition("Delphi");
                    case "nimrod":
                        return HighlightingManager.Instance.GetDefinition("Nimrod");
                    case "applescript":
                        return HighlightingManager.Instance.GetDefinition("AppleScript");
                    case "purebasic":
                        return HighlightingManager.Instance.GetDefinition("PureBasic");
                    case "autoit":
                        return HighlightingManager.Instance.GetDefinition("AutoIt");
                    case "dot":
                    case "graphviz":
                        return HighlightingManager.Instance.GetDefinition("DOT");
                    case "sed":
                        return HighlightingManager.Instance.GetDefinition("Sed");
                    case "abap":
                        return HighlightingManager.Instance.GetDefinition("ABAP");
                    case "postscript":
                    case "pscript":
                        return HighlightingManager.Instance.GetDefinition("PostScript");
                    case "maxscript":
                        return HighlightingManager.Instance.GetDefinition("MaxScript");
                    case "red":
                        return HighlightingManager.Instance.GetDefinition("Red");
                    case "mermaid":
                        return HighlightingManager.Instance.GetDefinition("Mermaid");
                    case "haml":
                        return HighlightingManager.Instance.GetDefinition("Haml");
                    case "puppet":
                        return HighlightingManager.Instance.GetDefinition("Puppet");
                    case "scheme":
                        return HighlightingManager.Instance.GetDefinition("Scheme");
                    case "janet":
                        return HighlightingManager.Instance.GetDefinition("Janet");
                    case "idris":
                        return HighlightingManager.Instance.GetDefinition("Idris");
                    case "zig":
                        return HighlightingManager.Instance.GetDefinition("Zig");
                    case "turing":
                        return HighlightingManager.Instance.GetDefinition("Turing");
                    case "pike":
                        return HighlightingManager.Instance.GetDefinition("Pike");
                    case "eiffel":
                        return HighlightingManager.Instance.GetDefinition("Eiffel");
                    case "julia":
                        return HighlightingManager.Instance.GetDefinition("Julia");
                    case "nasm":
                    case "assembly":
                    case "asm":
                        return HighlightingManager.Instance.GetDefinition("Assembly");
                    case "groff":
                    case "roff":
                        return HighlightingManager.Instance.GetDefinition("Groff");
                    case "bc":
                        return HighlightingManager.Instance.GetDefinition("BC");
                    case "gml":
                        return HighlightingManager.Instance.GetDefinition("GameMaker Language");
                    case "ampl":
                        return HighlightingManager.Instance.GetDefinition("AMPL");
                    case "sml":
                    case "standardml":
                        return HighlightingManager.Instance.GetDefinition("Standard ML");
                    case "blitzmax":
                        return HighlightingManager.Instance.GetDefinition("BlitzMax");
                    case "clarion":
                        return HighlightingManager.Instance.GetDefinition("Clarion");
                    case "modula-2":
                    case "modula":
                        return HighlightingManager.Instance.GetDefinition("Modula-2");
                    case "icon":
                        return HighlightingManager.Instance.GetDefinition("Icon");
                    case "genie":
                        return HighlightingManager.Instance.GetDefinition("Genie");
                    case "xslt":
                        return HighlightingManager.Instance.GetDefinition("XSLT");
                    case "boo":
                        return HighlightingManager.Instance.GetDefinition("Boo");
                    case "pawn":
                        return HighlightingManager.Instance.GetDefinition("Pawn");
                    case "povray":
                    case "pov":
                        return HighlightingManager.Instance.GetDefinition("POV-Ray");
                    case "rex":
                        return HighlightingManager.Instance.GetDefinition("REXX");
                    case "v":
                        return HighlightingManager.Instance.GetDefinition("V");
                    case "logtalk":
                        return HighlightingManager.Instance.GetDefinition("Logtalk");
                    case "factor":
                        return HighlightingManager.Instance.GetDefinition("Factor");
                    case "rexxt":
                        return HighlightingManager.Instance.GetDefinition("REXXT");
                    case "picat":
                        return HighlightingManager.Instance.GetDefinition("Picat");
                    case "algol":
                    case "algol60":
                    case "algol68":
                        return HighlightingManager.Instance.GetDefinition("ALGOL");
                    case "forth":
                        return HighlightingManager.Instance.GetDefinition("Forth");
                    case "coq":
                        return HighlightingManager.Instance.GetDefinition("Coq");
                    case "vb6":
                    case "visualbasic6":
                        return HighlightingManager.Instance.GetDefinition("Visual Basic 6");
                    case "bcpl":
                        return HighlightingManager.Instance.GetDefinition("BCPL");
                    case "promela":
                        return HighlightingManager.Instance.GetDefinition("Promela");
                    case "ml":
                        return HighlightingManager.Instance.GetDefinition("ML");
                    case "spice":
                        return HighlightingManager.Instance.GetDefinition("SPICE");
                    case "autohotkey":
                    case "ahk":
                        return HighlightingManager.Instance.GetDefinition("AutoHotkey");
                    case "capnproto":
                    case "capnp":
                        return HighlightingManager.Instance.GetDefinition("Cap'n Proto");
                    case "gherkin":
                        return HighlightingManager.Instance.GetDefinition("Gherkin");
                    case "idl":
                        return HighlightingManager.Instance.GetDefinition("IDL");
                    case "idl4":
                        return HighlightingManager.Instance.GetDefinition("IDL4");
                    case "grip":
                        return HighlightingManager.Instance.GetDefinition("GRIP");
                    case "plc":
                        return HighlightingManager.Instance.GetDefinition("PLC");
                    case "cnc":
                        return HighlightingManager.Instance.GetDefinition("CNC");
                    case "parrot":
                        return HighlightingManager.Instance.GetDefinition("Parrot");
                    default:
                        return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

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
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete the selected snippets?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
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
                    Debug.WriteLine("Snippet deletion was cancelled by the user.");
                }
            }
            else
            {
                MessageBox.Show("No snippets selected for deletion.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Debug.WriteLine("No snippets selected for deletion.");
            }
        }

        private void NewSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<codexDBContext>();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);

            var mainViewModel = (MainViewModel)DataContext;
            Settings createSettingsWindow = new Settings(optionsBuilder.Options, mainViewModel);
            createSettingsWindow.Show();
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

        private void SnippetsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
