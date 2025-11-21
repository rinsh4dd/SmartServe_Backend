using Dapper;
using System.Data;
using SmartServe.Application.DTOs;
using SmartServe.Application.Contracts.Repository;
public class InventoryRepository : IInventoryRepository
{
    private readonly IDbConnection _db;

    public InventoryRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<int> AddProductAsync(AddProductDtoStaff dto, int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "INSERT");
        p.Add("@PRODUCTNAME", dto.ProductName);
        p.Add("@UNQBC", dto.UNQBC);
        p.Add("@CATEGORYID", dto.CategoryId);
        p.Add("@UNITPRICE", dto.UnitPrice);
        p.Add("@COSTPRICE", dto.CostPrice);
        p.Add("@CREATEDBY", userId);

        return await _db.QueryFirstOrDefaultAsync<int>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> UpdateProductAsync(int productId, UpdateProductDto dto, int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "UPDATE");
        p.Add("@PRODUCTID", productId);
        p.Add("@PRODUCTNAME", dto.ProductName);
        p.Add("@UNQBC", dto.UNQBC);
        p.Add("@CATEGORYID", dto.CategoryId);
        p.Add("@UNITPRICE", dto.UnitPrice);
        p.Add("@COSTPRICE", dto.CostPrice);
        p.Add("@MODIFIEDBY", userId);

        return await _db.QueryFirstOrDefaultAsync<int>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETALL");

        return await _db.QueryAsync<ProductResponseDto>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(int productId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETBYID");
        p.Add("@PRODUCTID", productId);

        return await _db.QueryFirstOrDefaultAsync<ProductResponseDto>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> DeleteProductAsync(int productId, int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "DELETE");
        p.Add("@PRODUCTID", productId);
        p.Add("@DELETEDBY", userId);

        return await _db.QueryFirstOrDefaultAsync<int>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> StockInAsync(int productId, int qty, int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "STOCK_IN");
        p.Add("@PRODUCTID", productId);
        p.Add("@QUANTITYINSTOCK", qty);
        p.Add("@MODIFIEDBY", userId);

        return await _db.QueryFirstOrDefaultAsync<int>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> StockOutAsync(int productId, int qty, int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "STOCK_OUT");
        p.Add("@PRODUCTID", productId);
        p.Add("@QUANTITYINSTOCK", qty);
        p.Add("@MODIFIEDBY", userId);

        return await _db.QueryFirstOrDefaultAsync<int>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetLowStockAsync()
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "LOW_STOCK");

        return await _db.QueryAsync<ProductResponseDto>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<decimal> GetTotalInventoryValueAsync()
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "TOTAL_VALUE");

        return await _db.QueryFirstOrDefaultAsync<decimal>(
            "SP_PRODUCTS", p, commandType: CommandType.StoredProcedure);
    }
}
