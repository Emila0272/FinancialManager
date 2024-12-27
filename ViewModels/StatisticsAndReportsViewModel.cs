using FinancialManager.Models;
using FinancialManager.Storage;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace FinancialManager.ViewModels
{
    public class StatisticsAndReportsViewModel : INotifyPropertyChanged
    {
        private ChartValues<decimal> _incomeValues;
        private ChartValues<decimal> _expenseValues;
        private List<string> _dayLabels;
        private SeriesCollection _series;

        public event PropertyChangedEventHandler PropertyChanged;

        public ChartValues<decimal> IncomeValues
        {
            get => _incomeValues;
            set
            {
                _incomeValues = value;
                OnPropertyChanged(nameof(IncomeValues));
            }
        }

        public ChartValues<decimal> ExpenseValues
        {
            get => _expenseValues;
            set
            {
                _expenseValues = value;
                OnPropertyChanged(nameof(ExpenseValues));
            }
        }

        public List<string> DayLabels
        {
            get => _dayLabels;
            set
            {
                _dayLabels = value;
                OnPropertyChanged(nameof(DayLabels));
            }
        }

        public SeriesCollection Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public StatisticsAndReportsViewModel()
        {
            // Инициализация и обновление данных
            UpdateData();
        }

        private void UpdateData()
        {
            var storage = ServiceLocator.ServiceProvider.GetRequiredService<StorageService>();

            // Пример транзакций
            var transactions = storage.GetTransactions();

            // Группируем данные по дням
            var groupedByDay = transactions
                .GroupBy(t => t.Date.Date)  // Используем только дату без времени
                .Select(g => new
                {
                    Day = g.Key,
                    Income = g.Where(t => t.TransactionType == TransactionType.Income).Sum(t => t.Sum),
                    Expense = g.Where(t => t.TransactionType == TransactionType.Expenses).Sum(t => t.Sum)
                })
                .OrderBy(g => g.Day)
                .ToList();

            // Заполняем данные для графика
            DayLabels = groupedByDay.Select(g => g.Day.ToString("dd:MM:yyyy")).ToList();
            IncomeValues = new ChartValues<decimal>(groupedByDay.Select(g => g.Income));
            ExpenseValues = new ChartValues<decimal>(groupedByDay.Select(g => g.Expense));

            // Настроим Series для LiveCharts (столбцы доходов и расходов)
            Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Доходы",
                    Values = IncomeValues,
                    Fill = System.Windows.Media.Brushes.Blue
                },
                new ColumnSeries
                {
                    Title = "Расходы",
                    Values = ExpenseValues,
                    Fill = System.Windows.Media.Brushes.Red
                }
            };
        }

        // Метод для уведомления об изменении свойств
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
