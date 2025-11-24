using SmartServe.Application.DTOs;

namespace SmartServe.Application.Contracts.Repository
{
    public interface IVehichleRepository
    {
        Task<int> AddVehicleAsync(int customerId, int createdBy, CreateVehicleDto dto);

        Task<int> UpdateVehicleAsync(int userId, UpdateVehicleDto dto);
        Task<int> DeleteVehicleAsync(int vehicleId, int deletedBy);
        Task<IEnumerable<VehicleResponseDto>> GetAllVehiclesAsync();
        Task<VehicleResponseDto> GetVehicleByIdAsync(int vehicleId);
        Task<IEnumerable<VehicleResponseDto>> GetVehiclesByCustomerAsync(int customerId);
        Task<dynamic> GetVehicleHistoryAsync(int vehicleId);

    }
}