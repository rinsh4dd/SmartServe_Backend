using System.Data;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.Models.Auth;

public interface IAuthRepository
{
    Task<int> RegisterUserAsync(RegisterUserDto dto, IDbTransaction transaction);
    Task<UserModel?> GetUserByEmailAsync(string email);
    IDbConnection GetConnection();
    Task<int> RegisterUserAsyncNoTr(RegisterUserDto dto);
}
