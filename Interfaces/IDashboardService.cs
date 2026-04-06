using FinanceDashboardAPI.Models.DTOs;

namespace FinanceDashboardAPI.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetUserDashboard(int userId);
        Task<DashboardDTO> GetAllDashboard();
    }
}
