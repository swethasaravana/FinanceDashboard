using FinanceDashboardAPI.Context;
using FinanceDashboardAPI.Interfaces;
using FinanceDashboardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboardAPI.Repositories
{
    public class FinancialRecordRepository : IFinancialRecordRepository
    {
        private readonly AppDbContext _context;

        public FinancialRecordRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(FinancialRecord record)
        {
            await _context.FinancialRecords.AddAsync(record);
        }

        public async Task<List<FinancialRecord>> GetAll()
        {
            return await _context.FinancialRecords.ToListAsync();
        }

        public async Task<FinancialRecord?> GetById(int id)
        {
            return await _context.FinancialRecords.FindAsync(id);
        }
        public async Task<List<FinancialRecord>> GetByUserId(int userId)
        {
            return await _context.FinancialRecords
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
        public async Task DeleteRecord(FinancialRecord record)
        {
            _context.FinancialRecords.Remove(record);
        }
    }
}
