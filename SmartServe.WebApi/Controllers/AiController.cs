using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.Helpers;

namespace SmartServe.WebAPI.Controllers
{
    [ApiController]
    [Route("api/ai")]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [Authorize(Roles = "Admin,Staff,Technician,Customer")]
        [HttpGet("vehicle-profile/{vehicleId}")]
        public async Task<IActionResult> GetVehicleProfile(int vehicleId)
        {
            var result = await _aiService.GenerateVehicleAIReportAsync(vehicleId);
            return StatusCode(result.StatusCode, result);
        }
[Authorize]
    [HttpGet("currrent-user")]
    public IActionResult GetCurrentUser()
    {
        var dto = new CurrentUserDto
        {
            UserId = ClaimsHelper.GetUserId(User),
            UserName = ClaimsHelper.GetUserName(User),
            Email = ClaimsHelper.GetEmail(User),
            Role = ClaimsHelper.GetRole(User)
        };

        return Ok(new 
        {
            statusCode = 200,
            message = "Current user profile",
            data = dto
        });
    }

        
        [HttpGet("health")]
        [AllowAnonymous]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "OK",
                service = "AI Module",
                model = "gemini-2.5-flash",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
