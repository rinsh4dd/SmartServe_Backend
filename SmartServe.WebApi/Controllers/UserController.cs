using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.DTOs.UserDto;
using SmartServe.Application.Helpers;

namespace SmartServe.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            dto.CreatedBy = ClaimsHelper.GetUserId(User);
            var result = await _service.CreateUserAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await _service.GetUserByIdAsync(userId);
            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            dto.ModifiedBy = User.GetUserId();
            var result = await _service.UpdateUserAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(DeleteUserDto dto)
        {
            var adminId = ClaimsHelper.GetUserId(User);
            dto.DeletedBy = adminId;
            var result = await _service.DeleteUserAsync(dto);
            return StatusCode(result.StatusCode, result);
        }
        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            dto.UserId = ClaimsHelper.GetUserId(User);
            var response = await _service.ChangePasswordAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var response = await _service.ForgotPasswordAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var response = await _service.ResetPasswordAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
      
    }
}
