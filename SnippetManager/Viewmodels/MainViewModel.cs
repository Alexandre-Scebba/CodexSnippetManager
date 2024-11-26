using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using SnippetManager.Data;
using SnippetManager.Models;
using System.Configuration;
using System.Windows.Controls;

namespace SnippetManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static User _currentUser;
        public static User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                // Notify property changed if needed
            }
        }

        //added to allow null
        private string _username = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private readonly codexDBContext _context;
        private object _currentView = null!;
        private bool _isLoggedIn;
        private bool _rememberMe;

        //added
        public ObservableCollection<string> AvailableLanguages { get; set; } = new ObservableCollection<string>
        {
            "C#",
            "Java",
            "Python",
            "JavaScript",
            "HTML",
            "CSS"
        };

        //added
        // load additional languages dynamically from the database
        public void LoadLanguagesFromDatabase()
        {
            var dbLanguages = _context.Categories
                .Where(c => c.Type == "Language")
                .Select(c => c.Name)
                .Except(AvailableLanguages)
                .ToList();

            foreach (var lang in dbLanguages)
            {
                AvailableLanguages.Add(lang);
            }
            Debug.WriteLine($"Loaded languages: {string.Join(", ", dbLanguages)}");
        }

        public void ToggleLanguageSelection(string language)
        {
            if (SelectedLanguages.Contains(language))
            {
                SelectedLanguages.Remove(language);
            }
            else
            {
                SelectedLanguages.Add(language);
            }
            OnPropertyChanged(nameof(SelectedLanguages));
            OnPropertyChanged(nameof(SelectedLanguagesText));
        }



        public void LoadSnippet(Snippet snippet)
        {
            SelectedLanguages.Clear();
            var snippetLanguages = snippet.Language.Split(',').Select(lang => lang.Trim());
            foreach (var lang in snippetLanguages)
            {
                if (AvailableLanguages.Contains(lang))
                {
                    SelectedLanguages.Add(lang);
                }
            }
            OnPropertyChanged(nameof(SelectedLanguages));
            OnPropertyChanged(nameof(SelectedLanguagesText));
        }


        //tags
        private ObservableCollection<string> _availableTags = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableTags
        {
            get => _availableTags;
            set
            {
                _availableTags = value;
                OnPropertyChanged(nameof(AvailableTags));
            }
        }

        public void LoadTags(IEnumerable<string> tags)
        {
            AvailableTags.Clear();
            foreach (var tag in tags)
            {
                AvailableTags.Add(tag);
            }
        }

        //added
        // load tags dynamically from db
        public void LoadTagsFromDatabase()
        {
            var dbTags = _context.Categories
                .Where(c => c.Type == "Tag")
                .Select(c => c.Name)
                .Except(AvailableTags)
                .ToList();

            foreach (var tag in dbTags)
            {
                AvailableTags.Add(tag);
            }
            Debug.WriteLine($"Loaded tags: {string.Join(", ", dbTags)}");
        }

        private ObservableCollection<string> _selectedTags = new ObservableCollection<string>();
        public ObservableCollection<string> SelectedTags
        {
            get => _selectedTags;
            set
            {
                _selectedTags = value;
                OnPropertyChanged(nameof(SelectedTags));
            }
        }

        private ObservableCollection<string> _selectedLanguages = new ObservableCollection<string>();
        public ObservableCollection<string> SelectedLanguages
        {
            get => _selectedLanguages;
            set
            {
                _selectedLanguages = value;
                OnPropertyChanged(nameof(SelectedLanguages));
            }
        }

        public string SelectedLanguagesText => string.Join(", ", SelectedLanguages);
        public string SelectedTagsText => string.Join(", ", SelectedTags);


        //dynamic additions of language and tags
        //added error notify user if [] already exists

        public void AddCustomLanguage(string customLanguage)
        {
            if (AvailableLanguages.Contains(customLanguage))
            {
                MessageBox.Show("This language already exists.", "Duplicate Language", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            AvailableLanguages.Add(customLanguage);
        }

        public void AddCustomTag(string customTag)
        {
            if (AvailableTags.Contains(customTag))
            {
                MessageBox.Show("This tag already exists.", "Duplicate Tag", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            AvailableTags.Add(customTag);
            SelectedTags.Add(customTag);
        }

        //syntax highlight logic
        private Dictionary<string, string> SyntaxHighlightRules = new Dictionary<string, string>
        {
            { "C#", "C#" },
            { "Java", "Java" },
            { "Python", "Python" },
            { "JavaScript", "JavaScript" },
            { "HTML", "HTML" },
            { "CSS", "CSS" }
        };

        private string _selectedSyntaxHighlighting = "PlainText";
        public string SelectedSyntaxHighlighting
        {
            get => _selectedSyntaxHighlighting;
            set
            {
                _selectedSyntaxHighlighting = value;
                OnPropertyChanged(nameof(SelectedSyntaxHighlighting));
            }
        }

        public void ApplySyntaxHighlighting(string selectedLanguage)
        {
            if (SyntaxHighlightRules.TryGetValue(selectedLanguage, out var rulesPath))
            {
                SelectedSyntaxHighlighting = rulesPath;
            }
            else
            {
                SelectedSyntaxHighlighting = "PlainText";
            }
        }

        public MainViewModel()
        {
            //added 
            var optionsBuilder = new DbContextOptionsBuilder<codexDBContext>();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
            _context = new codexDBContext(optionsBuilder.Options);

            RegisterCommand = new RelayCommand(_ => Register());
            LoginCommand = new RelayCommand(_ => Login());
            ShowRegisterViewCommand = new RelayCommand(_ => ShowRegisterView());
            CurrentView = new DefaultView { DataContext = this };

            //set default syntax highlighting
            SelectedSyntaxHighlighting = "PlainText";

            // load available languages and tags from db
            LoadLanguagesFromDatabase();
            LoadTagsFromDatabase();

            // Load Remember Me info
            LoadRememberMeInfo();

            Debug.WriteLine("MainViewModel initialized.");
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                Debug.WriteLine($"Username set to: {_username}");
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                Debug.WriteLine($"Email set to: {_email}");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                Debug.WriteLine("Password set.");
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                Debug.WriteLine("ConfirmPassword set.");
            }
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
                Debug.WriteLine($"CurrentView set to: {_currentView.GetType().Name}");
            }
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
                Debug.WriteLine($"IsLoggedIn set to: {_isLoggedIn}");
            }
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                OnPropertyChanged(nameof(RememberMe));
                Debug.WriteLine($"RememberMe set to: {_rememberMe}");
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand ShowRegisterViewCommand { get; }

        private void Register()
        {
            Debug.WriteLine("Register command executed.");

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Invalid email format.");
                Debug.WriteLine("Invalid email format.");
                return;
            }

            if (!IsValidPassword(Password))
            {
                MessageBox.Show("Password must be at least 8 characters long, include an uppercase letter, a digit, and a special character.");
                Debug.WriteLine("Password does not meet requirements.");
                return;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                Debug.WriteLine("Passwords do not match.");
                return;
            }

            var hashedPassword = HashPassword(Password);
            var user = new User
            {
                Username = Username,
                Email = Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            MessageBox.Show("Registration successful.");
            Debug.WriteLine("Registration successful.");
            CurrentView = new DefaultView { DataContext = this };
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPassword(string password)
        {
            // Minimum 8 characters, at least one uppercase, one lowercase, one digit, one special character
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }

        private void Login()
        {
            Debug.WriteLine("Login command executed.");

            var hashedPassword = HashPassword(Password);
            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                IsLoggedIn = true;
                MainViewModel.CurrentUser = user; //set current user
                MessageBox.Show("Login successful.");
                Debug.WriteLine("Login successful.");

                if (RememberMe)
                {
                    string token = GenerateRememberMeToken();
                    SaveRememberMeInfo(user.Username, token);
                }

                // Switch to the main view or dashboard
                CurrentView = new Dashboard { DataContext = this };
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                Debug.WriteLine("Invalid username or password.");
            }
        }

        private void ShowRegisterView()
        {
            Debug.WriteLine("ShowRegisterView command executed.");
            CurrentView = new RegistrationView { DataContext = this };
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                Debug.WriteLine("Password hashed.");
                return builder.ToString();
            }
        }

        //added delegate to suppress null warning
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        //added
        public void ApplyHighlighting(string rulesPath)
        {
            // logic for applying syntax highlight rules
            Debug.WriteLine($"Applying syntax highlighting using rules from: {rulesPath}");
        }

        public void ApplyPlainText()
        {
            // logic for default plain text
            Debug.WriteLine("Applying plain text formatting.");
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.WriteLine($"PropertyChanged: {propertyName}");
        }

        private void SaveRememberMeInfo(string username, string token)
        {
            string filePath = "remember_me.txt";
            File.WriteAllText(filePath, $"{username}\n{token}");
        }

        private string GenerateRememberMeToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static void ClearRememberMeInfo()
        {
            string filePath = "remember_me.txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private void LoadRememberMeInfo()
        {
            string filePath = "remember_me.txt";
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length == 2)
                {
                    string savedUsername = lines[0];
                    string savedToken = lines[1];

                    var user = _context.Users.FirstOrDefault(u => u.Username == savedUsername);
                    if (user != null && !IsLoggedIn)
                    {
                        Username = user.Username;
                        IsLoggedIn = true;
                        MainViewModel.CurrentUser = user; // Set current user
                        CurrentView = new Dashboard { DataContext = this };
                        Debug.WriteLine("Logged in with Remember Me");
                    }
                }
            }
        }
    }
}
