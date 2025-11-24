using SmartServe.Application.DTOs.TechnicianDto;

namespace SmartServe.Application.Contracts.Repository
{
    public interface ITechnicianRepository
    {
        Task<int> CreateTechnicianAsync(int userId, int createdBy);
        Task<int> GetTechnicianIdByUserIdAsync(int userId);
        Task<IEnumerable<TechnicianResponseDto>> GetAllAsync();
        Task<TechnicianResponseDto?> GetByIdAsync(int technicianId);
        Task<TechnicianResponseDto?> GetByUserIdAsync(int userId);
        Task<bool> UpdateProfileAsync(int userId, UpdateTechnicianProfileDto dto, int modifiedBy);
        Task<bool> UpdateStatusAsync(int technicianId, string status, int modifiedBy);
        Task<bool> SetAvailabilityAsync(int technicianId, bool isAvailable, int modifiedBy);
        Task<bool> DeleteAsync(int technicianId, int deletedBy);
        Task<bool> RestoreAsync(int technicianId, int modifiedBy);
    }
}
