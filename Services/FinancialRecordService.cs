using FinanceDashboardAPI.Context;
using FinanceDashboardAPI.Interfaces;
using FinanceDashboardAPI.Models;
using FinanceDashboardAPI.Models.DTOs;

namespace FinanceDashboardAPI.Services
{
    public class FinancialRecordService : IFinancialRecordService
    {
        private readonly IFinancialRecordRepository _recordRepo;
        private readonly AppDbContext _context;

        public FinancialRecordService(IFinancialRecordRepository recordRepo, AppDbContext context)
        {
            _recordRepo = recordRepo;
            _context = context;
        }

        public async Task<string> AddRecord(CreateRecordDTO dto, int userId)
        {
            var record = new FinancialRecord
            {
                Amount = dto.Amount,
                Type = dto.Type,
                Category = dto.Category,
                Notes = dto.Notes,
                UserId = userId
            };

            await _recordRepo.Add(record);
            await _context.SaveChangesAsync();

            return "Record created successfully";
        }

        public async Task<List<RecordResponseDTO>> GetAllRecords()
        {
            var records = await _recordRepo.GetAll();

            return records.Select(r => new RecordResponseDTO
            {
                Id = r.Id,
                Amount = r.Amount,
                Type = r.Type,
                Category = r.Category,
                Date = r.Date
            }).ToList();
        }

        public async Task<RecordResponseDTO?> GetRecordById(int id)
        {
            var r = await _recordRepo.GetById(id);

            if (r == null) return null;

            return new RecordResponseDTO
            {
                Id = r.Id,
                Amount = r.Amount,
                Type = r.Type,
                Category = r.Category,
                Date = r.Date
            };
        }

        public async Task<List<RecordResponseDTO>> GetRecordsByUserId(int userId)
        {
            var records = await _recordRepo.GetByUserId(userId);

            return records.Select(r => new RecordResponseDTO
            {
                Id = r.Id,
                Amount = r.Amount,
                Type = r.Type,
                Category = r.Category,
                Date = r.Date
            }).ToList();
        }
        public async Task<string> UpdateRecord(int id, UpdateRecordDTO dto)
        {
            var record = await _recordRepo.GetById(id);

            if (record == null)
                return "Record not found";

            record.Amount = dto.Amount;
            record.Type = dto.Type;
            record.Category = dto.Category;
            record.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return "Record updated successfully";
        }

        public async Task<string> DeleteRecord(int id)
        {
            var record = await _recordRepo.GetById(id);

            if (record == null)
                return "Record not found";

            await _recordRepo.DeleteRecord(record);
            await _context.SaveChangesAsync();

            return "Record deleted successfully";
        }
    }
}
