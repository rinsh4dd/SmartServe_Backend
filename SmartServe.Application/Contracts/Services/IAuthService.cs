using SmartServe.Application.DTOs.AuthDto;
using System.Threading.Tasks;

namespace SmartServe.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<AuthReponseDto> RegisterUserAsync(RegisterUserDto dto);
        Task<AuthReponseDto> LoginUserAsync(LoginRequestDto dto );
        
    }
}
