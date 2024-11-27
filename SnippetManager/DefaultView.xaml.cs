using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SnippetManager.ViewModels;

namespace SnippetManager;

public partial class DefaultView : UserControl
{
    public DefaultView()
    {
        InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel) viewModel.Password = ((PasswordBox)sender).Password;
    }

    private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            if (DataContext is MainViewModel viewModel && viewModel.LoginCommand.CanExecute(null))
                viewModel.LoginCommand.Execute(null);
    }
}