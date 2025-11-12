using SmartServe.Application.DTOs.UserDto;

namespace SmartServe.Application.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<int> CreateUserAsync(CreateUserDto dto, string passwordHash);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(UpdateUserDto dto);
        Task DeleteUserAsync(DeleteUserDto dto);
        Task<string?> GetPasswordHashAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, string newHash);
        Task<int> SaveResetOtpAsync(string email, string otp, DateTime expiry);
        Task<(string? Otp, DateTime? Expiry)> GetResetOtpAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string newHash);

    }
}
