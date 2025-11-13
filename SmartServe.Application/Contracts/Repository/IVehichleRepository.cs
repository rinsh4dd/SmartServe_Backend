using SmartServe.Application.DTOs;

namespace SmartServe.Application.Contracts.Repository
{
    public interface IVehichleRepository
    {
        Task<int> AddVehicleAsync(int userId, int customerId, CreateVehicleDto dto);
        Task<int> UpdateVehicleAsync(int userId, UpdateVehicleDto dto);
        Task<int> DeleteVehicleAsync(int vehicleId, int userId);
        Task<IEnumerable<VehicleResponseDto>> GetAllVehiclesAsync();
        Task<VehicleResponseDto> GetVehicleByIdAsync(int vehicleId);
        Task<IEnumerable<VehicleResponseDto>> GetVehiclesByCustomerAsync(int customerId);
    }
}