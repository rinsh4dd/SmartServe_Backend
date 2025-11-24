using SmartServe.Application.DTOs;

namespace SmartServe.Application.Contracts.Repository
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<ProductResponseDto>> FilterProductsAsync(ProductFilterDto filter);
        Task<int> AddProductAsync(AddProductDtoStaff dto, int userId);
        Task<int> UpdateProductAsync(int productId, UpdateProductDto dto, int userId);
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto?> GetProductByIdAsync(int productId);
        Task<int> DeleteProductAsync(int productId, int userId);

        // Stock operations
        Task<int> StockInAsync(int productId, int qty, int userId);
        Task<int> StockOutAsync(int productId, int qty, int userId);

        // Analytics
        Task<IEnumerable<ProductResponseDto>> GetLowStockAsync();
        Task<decimal> GetTotalInventoryValueAsync();
    }

}