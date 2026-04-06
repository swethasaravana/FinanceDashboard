using FinanceDashboardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboardAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<FinancialRecord> FinancialRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User → Role (Many-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // User → FinancialRecords (One-to-Many)
            modelBuilder.Entity<FinancialRecord>()
                .HasOne(fr => fr.User)
                .WithMany(u => u.FinancialRecords)
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Decimal precision fix
            modelBuilder.Entity<FinancialRecord>()
                .Property(fr => fr.Amount)
                .HasPrecision(18, 2);
        }
    }
}
