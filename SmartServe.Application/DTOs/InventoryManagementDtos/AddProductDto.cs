namespace SmartServe.Application.DTOs
{
    public class AddProductDtoStaff
    {
        public string ProductName { get; set; }
        public string UNQBC { get; set; }   // Barcode / QR
        public int? CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public IFormFile? Image { get; set; }

    }

}