using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<IEnumerable<Customer>> GetCustomersWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCustomersCountAsync();
    }
}
