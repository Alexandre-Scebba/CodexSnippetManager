using System.Windows;
using System.Windows.Controls;

namespace SnippetManager
{
    public partial class DefaultView : UserControl
    {
        public DefaultView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}