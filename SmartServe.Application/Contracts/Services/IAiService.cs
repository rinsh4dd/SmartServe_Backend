using SmartServe.Application.Common;
using SmartServe.Application.DTOs;

namespace SmartServe.Application.Contracts.Services
{
    public interface IAIService
    {
        Task<ApiResponse<VehicleAIResponseDto>> GenerateVehicleAIReportAsync(int vehicleId);
    }
}
