using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.StaffDto;
using SmartServe.Application.Helpers;
using System.Security.Claims;

namespace SmartServe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("Invalid or missing user claim.");
            dto.CreatedBy = ClaimsHelper.GetUserId(User);
            var response = await _staffService.CreateStaffAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var response = await _staffService.GetAllStaffsAsync();
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _staffService.GetStaffByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStaffAsync(UpdateStaffDto dto)
        {
            dto.ModifiedBy = ClaimsHelper.GetUserId(User);
            var response = await _staffService.UpdateStaffAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteStaffAsync(DeleteStaffDto dto)
        {
            dto.DeletedBy = ClaimsHelper.GetUserId(User);
            var response = await _staffService.DeleteStaffAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
