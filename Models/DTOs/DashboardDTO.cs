namespace FinanceDashboardAPI.Models.DTOs
{
    public class DashboardDTO
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetBalance { get; set; }
        public Dictionary<string, decimal> CategoryTotals { get; set; }

    }
}
