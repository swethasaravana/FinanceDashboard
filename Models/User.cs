using System.ComponentModel.DataAnnotations;
using System.Data;

namespace FinanceDashboardAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<FinancialRecord> FinancialRecords { get; set; }
    }
}
