using System.Windows;
using System.Windows.Controls;
using SnippetManager.ViewModels;

namespace SnippetManager;

public partial class RegistrationView : UserControl
{
    public RegistrationView()
    {
        InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel) viewModel.Password = ((PasswordBox)sender).Password;
    }

    private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel) viewModel.ConfirmPassword = ((PasswordBox)sender).Password;
    }
}