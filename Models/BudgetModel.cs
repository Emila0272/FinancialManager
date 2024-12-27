namespace FinancialManager.Models
{
    public class BudgetModel
    {
        public double PlannedBudget { get; set; }
        public double TotalExpenses { get; set; }
        public double RemainingBudget => PlannedBudget - TotalExpenses;
    }
}
