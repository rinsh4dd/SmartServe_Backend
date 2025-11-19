using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.TechnicianDto;

namespace SmartServe.Application.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ITechnicianRepository _repo;

        public TechnicianService(ITechnicianRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<IEnumerable<TechnicianResponseDto>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return new ApiResponse<IEnumerable<TechnicianResponseDto>>(200, "Success", list);
        }

        public async Task<ApiResponse<TechnicianResponseDto>> GetByIdAsync(int technicianId)
        {
            var tech = await _repo.GetByIdAsync(technicianId);

            if (tech == null)
                return new ApiResponse<TechnicianResponseDto>(404, "Technician not found.");

            return new ApiResponse<TechnicianResponseDto>(200, "Success", tech);
        }

        public async Task<ApiResponse<TechnicianResponseDto>> GetByUserIdAsync(int userId)
        {
            var tech = await _repo.GetByUserIdAsync(userId);

            if (tech == null)
                return new ApiResponse<TechnicianResponseDto>(404, "Technician not found.");

            return new ApiResponse<TechnicianResponseDto>(200, "Success", tech);
        }

        public async Task<ApiResponse<int>> UpdateProfileAsync(int userId, UpdateTechnicianProfileDto dto, int modifiedBy)
        {
            var updated = await _repo.UpdateProfileAsync(userId, dto, modifiedBy);

            if (updated)
                return new ApiResponse<int>(400, "Update failed.");

            return new ApiResponse<int>(200, "Profile updated successfully.");
        }

        public async Task<ApiResponse<bool>> SetAvailabilityAsync(int technicianId, bool isAvailable, int modifiedBy)
        {
            var result = await _repo.SetAvailabilityAsync(technicianId, isAvailable, modifiedBy);

            if (!result)
                return new ApiResponse<bool>(400, "Unable to update availability.");

            return new ApiResponse<bool>(200, "Availability updated.", true);
        }

        public async Task<ApiResponse<bool>> UpdateStatusAsync(int technicianId, string status, int modifiedBy)
        {
            var result = await _repo.UpdateStatusAsync(technicianId, status, modifiedBy);

            if (!result)
                return new ApiResponse<bool>(400, "Unable to update status.");

            return new ApiResponse<bool>(200, "Status updated.", true);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int technicianId, int deletedBy)
        {
            var result = await _repo.DeleteAsync(technicianId, deletedBy);

            if (!result)
                return new ApiResponse<bool>(400, "Delete failed.");

            return new ApiResponse<bool>(200, "Technician soft-deleted.", true);
        }

        public async Task<ApiResponse<bool>> RestoreAsync(int technicianId, int modifiedBy)
        {
            var result = await _repo.RestoreAsync(technicianId, modifiedBy);

            if (!result)
                return new ApiResponse<bool>(400, "Restore failed.");

            return new ApiResponse<bool>(200, "Technician restored.", true);
        }
    }
}
