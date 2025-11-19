using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs;

namespace SmartServe.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehichleRepository _vehichleRepository;
        public VehicleService(IVehichleRepository vehichleRepository)
        {
            _vehichleRepository = vehichleRepository;
        }
        public async Task<ApiResponse<VehicleResponseDto>> GetVehicleById(int id)
        {
            var result = await _vehichleRepository.GetVehicleByIdAsync(id);
            if (result == null)
            {
                return new ApiResponse<VehicleResponseDto>(404, "Vehichle not found");
            }
            return new ApiResponse<VehicleResponseDto>(200, "vehicle fetched successfully", result);
        }

        public async Task<ApiResponse<IEnumerable<VehicleResponseDto>>> GetAllVehicles()
        {
            var result = await _vehichleRepository.GetAllVehiclesAsync();
            if (result == null)
            {
                return new ApiResponse<IEnumerable<VehicleResponseDto>>(404, "no vehichles found");
            }
            return new ApiResponse<IEnumerable<VehicleResponseDto>>(200, "vehicle fetched successfully", result);
        }

        public async Task<ApiResponse<IEnumerable<VehicleResponseDto>>> GetByCustomerId(int customerId)
        {
            var result = await _vehichleRepository.GetVehiclesByCustomerAsync(customerId);

            if (result == null || !result.Any())
            {
                return new ApiResponse<IEnumerable<VehicleResponseDto>>(
                    404,
                    $"No vehicles found for customer ID {customerId}"
                );
            }

            return new ApiResponse<IEnumerable<VehicleResponseDto>>(
                200,
                "Vehicles fetched successfully",
                result
            );
        }

        public async Task<ApiResponse<int>> DeleteVehichleAsync(int vehicleId, int deletedBy)
        {
            var data = await _vehichleRepository.DeleteVehicleAsync(vehicleId, deletedBy);
            if (data < 1 || data == 0)
            {
                return new ApiResponse<int>(404, "no vehichle found");
            }
            return new ApiResponse<int>(200, "Deleted Successfully");
        }


    }
}
