using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SnippetManager.Models;
using SnippetManager.ViewModels;

namespace SnippetManager;

/// <summary>
///     Interaction logic for Settings.xaml
/// </summary>
public partial class Settings : Window
{
    private readonly codexDBContext _context;
    private readonly MainViewModel _mainViewModel;

    public Settings(DbContextOptions<codexDBContext> options, MainViewModel mainViewModel)
    {
        InitializeComponent();
        _context = new codexDBContext(options);
        _mainViewModel = mainViewModel;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text;
        var oldPassword = OldPasswordBox.Password;
        var newPassword = NewPasswordBox.Password;
        var confirmPassword = ConfirmPasswordBox.Password;

        // Validation
        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(oldPassword) ||
            string.IsNullOrWhiteSpace(newPassword) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (newPassword != confirmPassword)
        {
            MessageBox.Show("New password and confirm password do not match.", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        // Update user settings
        var currentUser = MainViewModel.CurrentUser;
        if (currentUser != null)
        {
            if (currentUser.PasswordHash == HashPassword(oldPassword))
            {
                currentUser.Username = username;
                currentUser.PasswordHash = HashPassword(newPassword);
                // Save changes to the database
                _context.Users.Update(currentUser);
                _context.SaveChanges();

                MessageBox.Show("User account settings saved successfully.", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Old password is incorrect.", "Validation Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        else
        {
            MessageBox.Show("No user is currently logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        // Clear the current user and reset the application state
        MainViewModel.CurrentUser = null;
        MainViewModel.ClearRememberMeInfo();
        MessageBox.Show("You have been logged out.", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);

        // Switch to the login view
        _mainViewModel.CurrentView = new DefaultView { DataContext = _mainViewModel };
        Close();
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var b in bytes) builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}