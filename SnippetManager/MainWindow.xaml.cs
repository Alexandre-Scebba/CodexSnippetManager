using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using SnippetManager.Data;
using SnippetManager.Models;
using SnippetManager.ViewModels;

namespace SnippetManager
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly bool _isLoggedIn;

        public MainWindow() : this(null, false)
        {
            // This constructor calls the parameterized constructor
        }

        public MainWindow(ApplicationDbContext context, bool isLoggedIn)
        {
            InitializeComponent();
            _context = context ?? new ApplicationDbContext();
            _isLoggedIn = isLoggedIn;
            DataContext = new MainViewModel();

            CheckDatabaseConnection();
            Debug.WriteLine("MainWindow initialized.");
        }

        private void CheckDatabaseConnection()
        {
            if (_context.CanConnect())
            {
                MessageBox.Show("Database connection is available.");
                Debug.WriteLine("Database connection is available.");
            }
            else
            {
                MessageBox.Show("Failed to connect to the database.");
                Debug.WriteLine("Failed to connect to the database.");
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
                Debug.WriteLine("PasswordBox_PasswordChanged event triggered.");
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.ConfirmPassword = ((PasswordBox)sender).Password;
                Debug.WriteLine("ConfirmPasswordBox_PasswordChanged event triggered.");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                if (viewModel.Password != viewModel.ConfirmPassword)
                {
                    MessageBox.Show("Passwords do not match.");
                    Debug.WriteLine("Passwords do not match.");
                    return;
                }

                var hashedPassword = HashPassword(viewModel.Password);
                var user = new User
                {
                    Username = viewModel.Username,
                    PasswordHash = hashedPassword,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                MessageBox.Show("Registration successful.");
                Debug.WriteLine("Registration successful.");
            }
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
    }
}
