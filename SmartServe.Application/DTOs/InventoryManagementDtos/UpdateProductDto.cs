

public class UpdateProductDto
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? UNQBC { get; set; }
    public int? CategoryId { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public int? QuantityInStock { get; set; }
    public IFormFile? Image { get; set; }
    public bool? IsActive { get; set; }
}
