using System.ComponentModel.DataAnnotations;

namespace FinanceDashboardAPI.Models
{
    public class FinancialRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; } // Income / Expense

        [Required]
        public string Category { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [MaxLength(250)]
        public string? Notes { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
