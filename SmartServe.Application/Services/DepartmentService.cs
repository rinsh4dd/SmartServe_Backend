using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.DepartmentDto;
using SmartServe.Application.Models.Departments;
using SmartServe.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<ApiResponse<object>> CreateDepartmentAsync(CreateDepartmentDto dto)
        {
            var result = await _departmentRepository.CreateAsync(dto);

            if (result <= 0)
            {
                return new ApiResponse<object>(400, "Failed to create Department");
            }
            return new ApiResponse<object>(201, "Department Created Successfully");
        }
        public async Task<ApiResponse<IEnumerable<DepartmentModel>>> GetAllDepartments()
        {
            var data = await _departmentRepository.GetAllAsync();

            return new ApiResponse<IEnumerable<DepartmentModel>>(200, "Success", data);
        }

        public async Task<ApiResponse<object>> DeleteDepartmentAsync(int departmentId, int deletedBy)
        {
            var result = await _departmentRepository.DeleteAsync(departmentId, deletedBy);
            if (result <= 0)
            {
                return new ApiResponse<object>(400, "Failed to Delete Department");
            }
            return new ApiResponse<object>(200, "Department Deleted Successfully");
        }

        public async Task<ApiResponse<DepartmentModel>> GetDepartmentById(int id)
        {
            var data = await _departmentRepository.GetByIdAsync(id);
            if (data == null)
            {
                return new ApiResponse<DepartmentModel>(404, "Department not found!! ");
            }
            return new ApiResponse<DepartmentModel>(200, "Success", data);
        }

        public async Task<ApiResponse<Object>> UpdateDepartmentAsync(UpdateDepartmentDto dto)
        {

            var result = await _departmentRepository.UpdateAsync(dto);
            if (result <=0)
            {
                return new ApiResponse<object>(404, "Department not found or already deleted");

            }
            return new ApiResponse<object>(200, "Department updated successfully");
        }
    }
}
