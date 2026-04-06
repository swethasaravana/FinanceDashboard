namespace FinanceDashboardAPI.Models.DTOs
{
    public class CreateRecordDTO
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string? Notes { get; set; }
    }
}
