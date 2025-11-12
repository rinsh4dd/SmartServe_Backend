using SmartServe.Application.Common;
using SmartServe.Application.DTOs;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.DTOs.UserDto;

namespace SmartServe.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<ApiResponse<int>> CreateUserAsync(CreateUserDto dto);
        Task<ApiResponse<IEnumerable<UserResponseDto>>> GetAllAsync();
        Task<ApiResponse<object>> GetUserByIdAsync(int userId);
        Task<ApiResponse<UpdateUserDto>> UpdateUserAsync(UpdateUserDto dto);
        Task<ApiResponse<DeleteUserDto>> DeleteUserAsync(DeleteUserDto dto);
        Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordDto dto);
        Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto dto);
    }
}