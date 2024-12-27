using FinancialManager.Common;
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
    public class IncomeAndExpensesViewModel
    {
        private decimal _incomeAmount;
        private string _incomeCategory;
        private DateTime _incomeDate = DateTime.Now;
        private decimal _expenseAmount;
        private string _expenseCategory;
        private DateTime _expenseDate = DateTime.Now;

        private readonly StorageService _storageService;

        public decimal IncomeAmount
        {
            get => _incomeAmount;
            set
            {
                _incomeAmount = value;
                OnPropertyChanged(nameof(IncomeAmount));
            }
        }

        public string IncomeCategory
        {
            get => _incomeCategory;
            set
            {
                _incomeCategory = value;
                OnPropertyChanged(nameof(IncomeCategory));
            }
        }

        public DateTime IncomeDate
        {
            get => _incomeDate;
            set
            {
                _incomeDate = value;
                OnPropertyChanged(nameof(IncomeDate));
            }
        }

        public decimal ExpenseAmount
        {
            get => _expenseAmount;
            set
            {
                _expenseAmount = value;
                OnPropertyChanged(nameof(ExpenseAmount));
            }
        }

        public string ExpenseCategory
        {
            get => _expenseCategory;
            set
            {
                _expenseCategory = value;
                OnPropertyChanged(nameof(ExpenseCategory));
            }
        }

        public DateTime ExpenseDate
        {
            get => _expenseDate;
            set
            {
                _expenseDate = value;
                OnPropertyChanged(nameof(ExpenseDate));
            }
        }

        public ICommand SaveIncomeCommand { get; }
        public ICommand SaveExpenseCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action<string> ShowMessage;

        public IncomeAndExpensesViewModel()
        {
            IncomeDate = DateTime.Today;
            ExpenseDate = DateTime.Today;

            _storageService = ServiceLocator.ServiceProvider.GetRequiredService<StorageService>();

            SaveIncomeCommand = new RelayCommand(SaveIncome);
            SaveExpenseCommand = new RelayCommand(SaveExpense);
        }

        private void SaveIncome()
        {
            var transaction = new Models.TransactionModel
            {
                TransactionName = IncomeCategory,
                Sum = IncomeAmount,
                Date = IncomeDate,
                TransactionType = Models.TransactionType.Income
            };

            _storageService.AddTransaction(transaction);
            System.Diagnostics.Debug.WriteLine($"Доход сохранён: {IncomeAmount} {IncomeCategory} {IncomeDate}");
            ShowMessage?.Invoke($"Доход сохранён: {IncomeAmount} {IncomeCategory} {IncomeDate}");
        }

        private void SaveExpense()
        {
            var transaction = new Models.TransactionModel
            {
                TransactionName = ExpenseCategory,
                Sum = ExpenseAmount,
                Date = ExpenseDate,
                TransactionType = Models.TransactionType.Expenses
            };

            _storageService.AddTransaction(transaction);
            System.Diagnostics.Debug.WriteLine($"Расход сохранён: {ExpenseAmount} {ExpenseCategory} {ExpenseDate}");
            ShowMessage?.Invoke($"Расход сохранён: {ExpenseAmount} {ExpenseCategory} {ExpenseDate}");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
