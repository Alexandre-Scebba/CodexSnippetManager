using SnippetManager.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SnippetManager
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string oldPassword = OldPasswordBox.Password;
            string newPassword = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

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
                MessageBox.Show("New password and confirm password do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // space to handle updating the user settings in your application

            MessageBox.Show("User account settings saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the current user and reset the application state
            MainViewModel.CurrentUser = null;
            MainViewModel.ClearRememberMeInfo();
            MessageBox.Show("You have been logged out.", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);

            Application.Current.Shutdown();
        }
    }
}
