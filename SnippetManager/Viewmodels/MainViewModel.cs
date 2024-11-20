using System.Collections.ObjectModel;
using System.ComponentModel;
using SnippetManager.Models;

namespace SnippetManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private Snippet _newSnippet;
        private ObservableCollection<string> _languages;
        private ObservableCollection<string> _tags;
        private string _username;
        private string _password;
        private string _confirmPassword;

        public string SearchText { get; set; }
        public Snippet NewSnippet { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public ObservableCollection<string> Tags { get; set; }
        public string Username { get; set; }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}