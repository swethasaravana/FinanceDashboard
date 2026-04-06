
using FinanceDashboardAPI.Models.DTOs;

namespace FinanceDashboardAPI.Interfaces
{
    public interface IFinancialRecordService
    {
        Task<string> AddRecord(CreateRecordDTO dto, int userId);
        Task<List<RecordResponseDTO>> GetAllRecords();
        Task<RecordResponseDTO?> GetRecordById(int id);
        Task<List<RecordResponseDTO>> GetRecordsByUserId(int userId);
        Task<string> UpdateRecord(int id, UpdateRecordDTO dto);
        Task<string> DeleteRecord(int id);
    }
}
