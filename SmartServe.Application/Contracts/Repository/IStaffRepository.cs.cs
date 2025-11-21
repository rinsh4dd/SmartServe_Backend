using SmartServe.Application.DTOs.StaffDto;

namespace SmartServe.Application.Contracts.Repository
{
    public interface IStaffRepository
    {
        Task<int> GetStaffIdByUserIdAsync(int userId);
        Task<int> CreateStaffAsync(int userId, CreateStaffDto dto);
        Task<IEnumerable<object>> GetAllAsync();
        Task<dynamic> GetByIdAsync(int id);
        Task<int> UpdateAsync(UpdateStaffDto dto);
        Task<int> DeleteAsync(DeleteStaffDto dto);
        Task<int> GetStaffDepartmentAsync(int staffId);
        Task<int> GetDepartmentIdByUserIdAsync(int userId);
    }
}
