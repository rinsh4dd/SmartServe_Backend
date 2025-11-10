using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.Models.Auth;

public interface IAuthRepository
{
    Task<int> RegisterUserAsync(RegisterUserDto dto);
    Task<UserModel?> GetUserByEmailAsync(string email);
}
