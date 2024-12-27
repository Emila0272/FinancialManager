using FinancialManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FinancialManager.Views
{
    /// <summary>
    /// Логика взаимодействия для IncomeAndExpenses.xaml
    /// </summary>
    public partial class IncomeAndExpenses : Page
    {
        public IncomeAndExpenses(IncomeAndExpensesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.ShowMessage += ShowMessageBox;
        }

        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }
    }
}
