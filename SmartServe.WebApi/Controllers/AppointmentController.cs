using System.Security.Claims;
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
        private readonly CustomerHelper _customerHelper;
        public AppointmentController(IAppointmentService appointmentService, CustomerHelper customerHelper)
        {
            _appointmentService = appointmentService;
            _customerHelper = customerHelper;

        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateAppointmentCustomer([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = ClaimsHelper.GetUserId(User);
            var customerId = await _customerHelper.GetCustomerIdAsync(userId);

            if (customerId == 0)
                return BadRequest("Customer profile not found.");

            var result = await _appointmentService.CreateAppointmentAsync(dto, userId, customerId);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("slots/{date}")]
        public async Task<IActionResult> GetWindows(DateTime date)
        {
            var result = await _appointmentService.GetWindowsAsync(date);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("assign-tech")]
        public async Task<IActionResult> AssignTech(AssignTechDto dto)
        {
            int staffUserId = ClaimsHelper.GetUserId(User);
            var result = await _appointmentService.AssignTechnicianAsync(dto, staffUserId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _appointmentService.GetAppointmentById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var response = await _appointmentService.GetAllAppointments();
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            int userId = ClaimsHelper.GetUserId(User);

            var result = await _appointmentService.CancelAppointmentAsync(id, userId);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            int userId = ClaimsHelper.GetUserId(User);
            var result = await _appointmentService.GetHistoryAsync(userId);
            return StatusCode(result.StatusCode, result);
        }


    }
}

