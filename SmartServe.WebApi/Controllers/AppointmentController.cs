using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs;
using SmartServe.Application.Helpers;

namespace SmartServe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int createdBy = ClaimsHelper.GetUserId(User);

            var response = await _appointmentService.CreateAppointmentAsync(dto, createdBy);
            return StatusCode(response.StatusCode, response);


        }
    }
}

