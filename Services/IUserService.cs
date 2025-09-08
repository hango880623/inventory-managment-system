using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ValidateUserAsync(string email, string password);
        Task<IEnumerable<User>> GetUsersWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> GetTotalUsersCountAsync();
    }
}
