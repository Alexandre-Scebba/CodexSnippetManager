using System.ComponentModel;
using SnippetManager.Models;
using System.Diagnostics;

namespace SnippetManager.ViewModels
{
    public class SnippetViewModel : INotifyPropertyChanged
    {
        private bool _isSelected;

        public Snippet Snippet { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                    Debug.WriteLine($"Snippet '{Snippet.Title}' selection changed to: {_isSelected}");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}