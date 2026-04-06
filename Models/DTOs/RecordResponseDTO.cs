namespace FinanceDashboardAPI.Models.DTOs
{
    public class RecordResponseDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
