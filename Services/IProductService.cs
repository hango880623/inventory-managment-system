using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> GetTotalProductsCountAsync();
    }
}
