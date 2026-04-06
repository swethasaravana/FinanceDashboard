using FinanceDashboardAPI.Models.DTOs;

namespace FinanceDashboardAPI.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDTO dto);
        Task<string> Login(LoginDTO dto);
        Task<string> CreateUserByAdmin(CreateUserByAdminDTO dto);
        Task<List<UserResponseDTO>> GetAllUsers();
        Task<string> ToggleUserStatus(int userId);
        Task<string> UpdateUserRole(int userId, UpdateUserRoleDTO dto);
    }
}
