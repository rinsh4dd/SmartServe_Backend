using SmartServe.Application.Common;
using SmartServe.Application.DTOs.DepartmentDto;
using SmartServe.Application.Models.Departments;

namespace SmartServe.Application.Contracts.Services
{

    public interface IDepartmentService
    {
        Task<ApiResponse<object>> CreateDepartmentAsync(CreateDepartmentDto dto);
        Task<ApiResponse<IEnumerable< DepartmentModel>>> GetAllDepartments();
        Task<ApiResponse<object>> DeleteDepartmentAsync(int departmentId, int deletedBy);
        Task<ApiResponse<DepartmentModel>> GetDepartmentById(int id);
        Task<ApiResponse<object>> UpdateDepartmentAsync(UpdateDepartmentDto dto);

    }

}
