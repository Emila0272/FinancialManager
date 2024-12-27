using System.Windows.Input;
using FinancialManager.Common;
using FinancialManager.Views;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.ViewModels
{
    public class NavBarViewModel
    {
        private readonly CurrentPage _currentPage;

        public ICommand NavigateToIncomeAndExpensesCommand { get; }
        public ICommand NavigateToBudgetPlanningCommand { get; }
        public ICommand NavigateToStatisticsCommand { get; }

        public NavBarViewModel(CurrentPage currentPage)
        {
            _currentPage = currentPage;

            NavigateToIncomeAndExpensesCommand = new RelayCommand(NavigateToIncomeAndExpenses);
            NavigateToBudgetPlanningCommand = new RelayCommand(NavigateToBudgetPlanning);
            NavigateToStatisticsCommand = new RelayCommand(NavigateToStatistics);
        }

        private void NavigateToIncomeAndExpenses()
        {
            _currentPage.Current = ServiceLocator.ServiceProvider.GetRequiredService<IncomeAndExpenses>();
        }

        private void NavigateToBudgetPlanning()
        {
            _currentPage.Current = ServiceLocator.ServiceProvider.GetRequiredService<BudgetPlanning>();
        }

        private void NavigateToStatistics()
        {
            _currentPage.Current = ServiceLocator.ServiceProvider.GetRequiredService<StatisticsAndReports>();
        }
    }
}
