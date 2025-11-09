using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.AuthDto;

namespace SmartServe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.RegisterUserAsync(dto);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _authService.LoginUserAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
