using FinanceDashboardAPI.Context;
using FinanceDashboardAPI.Interfaces;
using FinanceDashboardAPI.Models;
using FinanceDashboardAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboardAPI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        // 👤 USER DASHBOARD
        public async Task<DashboardDTO> GetUserDashboard(int userId)
        {
            var records = await _context.FinancialRecords
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return CalculateDashboard(records);
        }

        public async Task<DashboardDTO> GetAllDashboard()
        {
            var records = await _context.FinancialRecords.ToListAsync();
            return CalculateDashboard(records);
        }

        private DashboardDTO CalculateDashboard(List<FinancialRecord> records)
        {
            var income = records
                .Where(r => r.Type == "Income")
                .Sum(r => r.Amount);

            var expense = records
                .Where(r => r.Type == "Expense")
                .Sum(r => r.Amount);

            var categoryTotals = records
                .GroupBy(r => r.Category)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Amount));

            return new DashboardDTO
            {
                TotalIncome = income,
                TotalExpense = expense,
                NetBalance = income - expense,
                CategoryTotals = categoryTotals
            };
        }
    }
}
