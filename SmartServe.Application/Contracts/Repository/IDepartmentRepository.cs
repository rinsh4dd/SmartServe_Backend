using SmartServe.Application.DTOs.DepartmentDto;
using SmartServe.Application.Models.Departments;

namespace SmartServe.Infrastructure.Repositories
{
    public interface IDepartmentRepository
    {
        Task<int> CreateAsync(CreateDepartmentDto dto);
        Task<int> UpdateAsync(UpdateDepartmentDto dto);
        Task<int> DeleteAsync(int departmentId, int deletedBy);
        Task<IEnumerable<DepartmentModel>> GetAllAsync();
        Task<DepartmentModel?> GetByIdAsync(int id);
    }

}
