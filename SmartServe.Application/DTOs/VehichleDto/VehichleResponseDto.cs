namespace SmartServe.Application.DTOs
{
    public class VehicleResponseDto
    {
        public int VehicleId { get; set; }
        public string CustomerName { get; set; }
        public string VehicleNumber { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Description { get; set; }
        public string FuelType { get; set; }
        public string Brand { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}