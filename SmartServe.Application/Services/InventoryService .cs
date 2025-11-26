using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.DTOs;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _repo;
    private readonly ICloudinaryService _cloudnary;
    public InventoryService(IInventoryRepository repo, ICloudinaryService cloudinaryService)
    {
        _repo = repo;
        _cloudnary = cloudinaryService;
    }

    private bool CanManageInventory(string role, int departmentId)
    {
        if (role == "Admin")
            return true;

        if (departmentId == 8)
            return true;

        return false;
    }


    public async Task<ApiResponse<int>> AddProductAsync(
    AddProductRequest request,
    int userId,
    string role,
    int staffId,
    int departmentId)
    {
        if (!CanManageInventory(role, departmentId))
            return new ApiResponse<int>(403, "Unauthorized: Only Inventory staff or Admin can add products.");

        if (string.IsNullOrWhiteSpace(request.ProductName))
            return new ApiResponse<int>(400, "Product name is required.");

        string imageUrl = null;

        if (request.Image != null)
        {
            using var stream = request.Image.OpenReadStream();
            imageUrl = await _cloudnary.UploadImageAsync(stream, request.Image.FileName);
        }

        var dto = new AddProductDtoStaff
        {
            ProductName = request.ProductName,
            UNQBC = request.UNQBC,
            CategoryId = request.CategoryId,
            UnitPrice = request.UnitPrice,
            CostPrice = request.CostPrice,
            ImageUrl = imageUrl
        };

        int id = await _repo.AddProductAsync(dto, userId);

        return new ApiResponse<int>(200, $"Product added successfully", id);
    }
    public async Task<ApiResponse<int>> UpdateProductAsync(
    int productId,
    UpdateProductDto dto,
    int userId,
    string role,
    int staffId,
    int deptId)
    {
        if (!CanManageInventory(role, deptId))
            return new ApiResponse<int>(403, "Unauthorized.");

        int rows = await _repo.UpdateProductAsync(productId, dto, userId);

        if (rows <= 0)
            return new ApiResponse<int>(404, "Product not found or update failed.");

        return new ApiResponse<int>(200, "Product updated.", rows);
    }

    public async Task<ApiResponse<int>> DeleteProductAsync(int productId, int userId, string role, int staffId, int deptId)
    {
        if (!CanManageInventory(role, deptId))
            return new ApiResponse<int>(403, "Unauthorized.");

        int rows = await _repo.DeleteProductAsync(productId, userId);
        if (rows <= 0)
            return new ApiResponse<int>(404, "Delete failed.");

        return new ApiResponse<int>(200, "Product deleted.", rows);
    }

    public async Task<ApiResponse<int>> StockInAsync(int productId, int qty, int userId, string role, int staffId, int deptId)
    {
        if (!CanManageInventory(role, deptId))
            return new ApiResponse<int>(403, "Unauthorized.");

        if (qty <= 0)
            return new ApiResponse<int>(400, "Quantity must be > 0.");

        int rows = await _repo.StockInAsync(productId, qty, userId);
        return new ApiResponse<int>(200, "Stock added successfully.", rows);
    }

    public async Task<ApiResponse<int>> StockOutAsync(int productId, int qty, int userId, string role, int staffId, int deptId)
    {
        if (!CanManageInventory(role, deptId))
            return new ApiResponse<int>(403, "Unauthorized.");

        if (qty <= 0)
            return new ApiResponse<int>(400, "Quantity must be > 0.");

        int rows = await _repo.StockOutAsync(productId, qty, userId);
        if (rows <= 0)
            return new ApiResponse<int>(400, "Stock deduction failed (insufficient stock).");

        return new ApiResponse<int>(200, "Stock reduced.", rows);
    }

    public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetAllProductsAsync()
    {
        var list = await _repo.GetAllProductsAsync();
        return new ApiResponse<IEnumerable<ProductResponseDto>>(200, "Success", list);
    }

    public async Task<ApiResponse<ProductResponseDto>> GetProductByIdAsync(int productId)
    {
        var product = await _repo.GetProductByIdAsync(productId);
        if (product == null)
            return new ApiResponse<ProductResponseDto>(404, "Product not found");

        return new ApiResponse<ProductResponseDto>(200, "Success", product);
    }

    public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetLowStockAsync()
    {
        var result = await _repo.GetLowStockAsync();
        return new ApiResponse<IEnumerable<ProductResponseDto>>(200, "Success", result);
    }

    public async Task<ApiResponse<decimal>> GetTotalValueAsync()
    {
        var total = await _repo.GetTotalInventoryValueAsync();
        return new ApiResponse<decimal>(200, "Success", total);
    }
    public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> FilterProductsAsync(ProductFilterDto filter)
    {
        var list = await _repo.FilterProductsAsync(filter);
        return new ApiResponse<IEnumerable<ProductResponseDto>>(200, "Success", list);
    }

}
