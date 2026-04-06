namespace FinanceDashboardAPI.Models.DTOs
{
    public class UpdateRecordDTO
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } // Income / Expense
        public string Category { get; set; }
        public string? Notes { get; set; }
    }
}
