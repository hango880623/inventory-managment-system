using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task<Supplier> CreateSupplierAsync(Supplier supplier);
        Task<Supplier> UpdateSupplierAsync(Supplier supplier);
        Task<bool> DeleteSupplierAsync(int id);
        Task<IEnumerable<Supplier>> GetSuppliersWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> GetTotalSuppliersCountAsync();
    }
}
