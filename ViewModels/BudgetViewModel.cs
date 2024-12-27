using FinancialManager.Common;
using FinancialManager.Models;
using FinancialManager.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinancialManager.ViewModels
{
    public class BudgetViewModel : INotifyPropertyChanged
    {
        private BudgetModel _budgetModel;
        private double _newExpense;

        

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddExpenseCommand { get; }

        public BudgetViewModel()
        {
            

            _budgetModel = new BudgetModel();
            AddExpenseCommand = new RelayCommand(AddExpense);
        }

        public double PlannedBudget
        {
            get => _budgetModel.PlannedBudget;
            set
            {
                _budgetModel.PlannedBudget = value;
                OnPropertyChanged(nameof(PlannedBudget));
                OnPropertyChanged(nameof(RemainingBudget));
            }
        }

        public double NewExpense
        {
            get => _newExpense;
            set
            {
                _newExpense = value;
                OnPropertyChanged(nameof(NewExpense));
            }
        }

        public double TotalExpenses
        {
            get => _budgetModel.TotalExpenses;
            set
            {
                _budgetModel.TotalExpenses = value;
                OnPropertyChanged(nameof(TotalExpenses));
                OnPropertyChanged(nameof(RemainingBudget));
            }
        }

        public double RemainingBudget => _budgetModel.RemainingBudget;

        public void AddExpense()
        {
            TotalExpenses += NewExpense;
            NewExpense = 0; 
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
