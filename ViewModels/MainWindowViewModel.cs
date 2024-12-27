using FinancialManager.Common;
using FinancialManager.CustomUserControls;
using System.ComponentModel;
using System.Windows.Controls;

namespace FinancialManager.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly CurrentPage _currentPageService;

        public UserControl NavBar { get; set; }

        public Page CurrentPage
        {
            get => _currentPageService.Current;
            set
            {
                if (_currentPageService.Current != value)
                {
                    _currentPageService.Current = value;
                    OnPropertyChanged(nameof(CurrentPage));  // Уведомление об изменении CurrentPage
                }
            }
        }

        public MainWindowViewModel(CurrentPage currentPageService, NavBar navBar)
        {
            _currentPageService = currentPageService;
            // Подписка на изменения CurrentPage
            _currentPageService.PropertyChanged += CurrentPageService_PropertyChanged;

            // Инициализация с текущей страницей
            CurrentPage = _currentPageService.Current;
            NavBar = navBar;
        }

        private void CurrentPageService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
             OnPropertyChanged(nameof(CurrentPage));          
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
