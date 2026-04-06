using FinanceDashboardAPI.Models;

namespace FinanceDashboardAPI.Interfaces
{
    public interface IFinancialRecordRepository
    {
        Task Add(FinancialRecord record);
        Task<List<FinancialRecord>> GetAll();
        Task<FinancialRecord?> GetById(int id);
        Task<List<FinancialRecord>> GetByUserId(int userId);
        Task DeleteRecord(FinancialRecord record);
    }
}
