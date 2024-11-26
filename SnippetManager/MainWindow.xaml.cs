using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using SnippetManager.Data;
using SnippetManager.Models;
using SnippetManager.ViewModels;
using System.Configuration;
using System.Windows.Media.Imaging;


namespace SnippetManager
{
    public partial class MainWindow : Window
    {
        private readonly codexDBContext _context = null!;
        private readonly bool _isLoggedIn;

        //added codexdb EF Core fix
        public MainWindow() : this(
            new codexDBContext(new DbContextOptionsBuilder<codexDBContext>()
            .UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
            .Options), false
            )
        {
            // This constructor calls the parameterized constructor
        }

        public MainWindow(codexDBContext context, bool isLoggedIn)
        {
            InitializeComponent();
            //added 
            // configure DbContext
            var optionsBuilder = new DbContextOptionsBuilder<codexDBContext>();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
            _context = new codexDBContext(optionsBuilder.Options);

            _isLoggedIn = isLoggedIn;
            DataContext = new MainViewModel();

            CheckDatabaseConnection();
            Debug.WriteLine("MainWindow initialized.");
        }

        //added a-sync run to avoid block UI thread
        private async void CheckDatabaseConnection()
        {
            try
            {
                bool canConnect = await Task.Run(() => _context.Database.CanConnect());
                if (canConnect)
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking database connection: {ex.Message}\n{ex.InnerException?.Message}");
                Debug.WriteLine($"Error checking database connection: {ex.Message}\n{ex.InnerException?.Message}");
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
                //added validation
                if (string.IsNullOrWhiteSpace(viewModel.Username) || string.IsNullOrWhiteSpace(viewModel.Password))
                {
                    MessageBox.Show("Username and Password cannot be empty.");
                    Debug.WriteLine("Username or Password is empty.");
                    return;
                }

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
