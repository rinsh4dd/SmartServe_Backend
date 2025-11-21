public class ProductResponseDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string UNQBC { get; set; }
    public string CategoryName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal CostPrice { get; set; }
    public int QuantityInStock { get; set; }
    public decimal TotalValue { get; set; }
    public string? ImageUrl { get; set; }

}
