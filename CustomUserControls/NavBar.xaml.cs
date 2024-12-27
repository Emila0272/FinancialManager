using FinancialManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinancialManager.CustomUserControls
{
    /// <summary>
    /// Логика взаимодействия для NavBar.xaml
    /// </summary>
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            var viewModel = ServiceLocator.ServiceProvider.GetRequiredService<NavBarViewModel>();
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
