using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SnippetManager.Data;

namespace SnippetManager
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public MainWindow() : this(new ApplicationDbContext("Server=tcp:codex-db.database.windows.net,1433;Initial Catalog=codex;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";"))
        {
            // This constructor calls the parameterized constructor
        }

        public MainWindow(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            DataContext = new MainViewModel();

            if (_context.CanConnect())
            {
                MessageBox.Show("Database connection is available.");
            }
            else
            {
                MessageBox.Show("Failed to connect to the database.");
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
    }


    public class MainViewModel : INotifyPropertyChanged
    {
        private string _password = string.Empty;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}