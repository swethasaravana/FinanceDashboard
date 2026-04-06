using FinanceDashboardAPI.Models;

namespace FinanceDashboardAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task AddUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User?> GetById(int id);
    }
}
