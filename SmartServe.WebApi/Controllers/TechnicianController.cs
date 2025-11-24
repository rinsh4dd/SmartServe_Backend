using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.TechnicianDto;
using SmartServe.Application.Helpers;
using System.Security.Claims;

namespace SmartServe.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechniciansController : ControllerBase
    {
        private readonly ITechnicianService _service;
        public TechniciansController(ITechnicianService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("my_profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var response = await _service.GetByUserIdAsync(userId);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("me/update-profile")]
        public async Task<IActionResult> UpdateProfile(UpdateTechnicianProfileDto dto)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            int modifiedBy = userId;
            var response = await _service.UpdateProfileAsync(userId, dto, modifiedBy);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            int modifiedBy = ClaimsHelper.GetUserId(User);
            var response = await _service.UpdateStatusAsync(id, status, modifiedBy);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("{id}/availability")]
        public async Task<IActionResult> SetAvailability(int id, [FromBody] bool isAvailable)
        {
            int modifiedBy = ClaimsHelper.GetUserId(User);
            var response = await _service.SetAvailabilityAsync(id, isAvailable, modifiedBy);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int deletedBy = ClaimsHelper.GetUserId(User);

            var response = await _service.DeleteAsync(id, deletedBy);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            int modifiedBy = ClaimsHelper.GetUserId(User);

            var response = await _service.RestoreAsync(id, modifiedBy);
            return StatusCode(response.StatusCode, response);
        }
    }
}
