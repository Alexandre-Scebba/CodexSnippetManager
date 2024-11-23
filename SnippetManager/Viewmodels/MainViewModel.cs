using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using SnippetManager.Data;
using SnippetManager.Models;

namespace SnippetManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private readonly ApplicationDbContext _context;
        private object _currentView;
        private bool _isLoggedIn;

        public MainViewModel()
        {
            _context = new ApplicationDbContext();
            RegisterCommand = new RelayCommand(_ => Register());
            LoginCommand = new RelayCommand(_ => Login());
            ShowRegisterViewCommand = new RelayCommand(_ => ShowRegisterView());
            CurrentView = new DefaultView { DataContext = this };

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

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand ShowRegisterViewCommand { get; }

        private void Register()
        {
            Debug.WriteLine("Register command executed.");

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

        private void Login()
        {
            Debug.WriteLine("Login command executed.");

            var hashedPassword = HashPassword(Password);
            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                IsLoggedIn = true;
                MessageBox.Show("Login successful.");
                Debug.WriteLine("Login successful.");
                // Switch to the main view or dashboard
                // CurrentView = new MainView { DataContext = this };
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.WriteLine($"PropertyChanged: {propertyName}");
        }
    }
}
