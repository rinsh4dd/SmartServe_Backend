using SmartServe.Application.Common;
using SmartServe.Application.DTOs;

namespace SmartServe.Application.Contracts.Services
{
    public interface IVehicleService
    {
        Task<ApiResponse<VehicleResponseDto>> GetVehicleById(int id);
        Task<ApiResponse<IEnumerable<VehicleResponseDto>>> GetAllVehicles();
        Task<ApiResponse<IEnumerable<VehicleResponseDto>>> GetByCustomerId(int id);
        Task<ApiResponse<int>> DeleteVehichleAsync(int vehicleId,int deletedBy);
        Task<ApiResponse<dynamic>> GetVehicleHistoryAsync(int vehicleId, string role);
    }
}