using System.ComponentModel.DataAnnotations;

namespace FinanceDashboardAPI.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }   // Viewer, Analyst, Admin

        public ICollection<User> Users { get; set; }   // Navigation

    }
}
