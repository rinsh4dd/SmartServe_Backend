using SmartServe.Application.Common;
using SmartServe.Application.DTOs.TechnicianDto;

namespace SmartServe.Application.Contracts.Services
{
    public interface ITechnicianService
    {
        Task<ApiResponse<IEnumerable<TechnicianResponseDto>>> GetAllAsync();
        Task<ApiResponse<TechnicianResponseDto>> GetByIdAsync(int technicianId);
        Task<ApiResponse<TechnicianResponseDto>> GetByUserIdAsync(int userId);
        Task<ApiResponse<int>> UpdateProfileAsync(int userId, UpdateTechnicianProfileDto dto, int modifiedBy);
        Task<ApiResponse<bool>> UpdateStatusAsync(int technicianId, string status, int modifiedBy);
        Task<ApiResponse<bool>> SetAvailabilityAsync(int technicianId, bool isAvailable, int modifiedBy);

        Task<ApiResponse<bool>> DeleteAsync(int technicianId, int deletedBy);
        Task<ApiResponse<bool>> RestoreAsync(int technicianId, int modifiedBy);
    }
}
