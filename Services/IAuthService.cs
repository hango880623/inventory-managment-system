using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtTokenAsync(User user);
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task<bool> IsEmailAvailableAsync(string email);
        Task<User> RegisterUserAsync(string name, string email, string password);
    }
}
