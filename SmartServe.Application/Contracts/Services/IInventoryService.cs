using SmartServe.Application.Common;
using SmartServe.Application.DTOs;

public interface IInventoryService
{
    Task<ApiResponse<int>> AddProductAsync(AddProductDtoStaff dto, int userId, string role, int staffId, int departmentId);
    Task<ApiResponse<int>> UpdateProductAsync(int productId, UpdateProductDto dto, int userId, string role, int staffId, int deptId);
    Task<ApiResponse<int>> DeleteProductAsync(int productId, int userId, string role, int staffId,int deptId);

    Task<ApiResponse<int>> StockInAsync(int productId, int qty, int userId, string role, int staffId, int deptId);
    Task<ApiResponse<int>> StockOutAsync(int productId, int qty, int userId, string role, int staffId);

    Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetAllProductsAsync();
    Task<ApiResponse<ProductResponseDto>> GetProductByIdAsync(int productId);

    Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetLowStockAsync();
    Task<ApiResponse<decimal>> GetTotalValueAsync();
}
