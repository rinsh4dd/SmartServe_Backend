using SmartServe.Application.Common;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.DTOs.StaffDto;

namespace SmartServe.Application.Contracts.Services
{
    public interface IStaffService
    {
        Task<AuthReponseDto> CreateStaffAsync(CreateStaffDto dto);
        Task<ApiResponse<IEnumerable<object>>> GetAllStaffsAsync();
        Task<ApiResponse<dynamic>> GetStaffByIdAsync(int id);
        Task<ApiResponse<int>> UpdateStaffAsync(UpdateStaffDto dto);
        Task<ApiResponse<dynamic>> DeleteStaffAsync(DeleteStaffDto dto);
    }
}
