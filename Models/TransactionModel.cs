using System;

namespace FinancialManager.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string TransactionName { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
    }

    public enum TransactionType
    {
        Income,
        Expenses
    }
}
