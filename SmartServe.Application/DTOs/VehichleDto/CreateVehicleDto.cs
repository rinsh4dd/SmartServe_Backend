using SmartServe.Domain.Enums;

namespace SmartServe.Application.DTOs
{
    public class CreateVehicleDto
    {
        public string VehicleNumber { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Description { get; set; }
        public FuelType FuelType { get; set; }
        public string Brand { get; set; }
    }
}