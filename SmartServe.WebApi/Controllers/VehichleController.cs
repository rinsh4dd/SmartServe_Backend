using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.DTOs;
using SmartServe.Application.Helpers;
using SmartServe.Application.Contracts.Repository;
using System;
using System.Threading.Tasks;
using SmartServe.Application.Contracts.Services;
using System.Security.Claims;

namespace SmartServe.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehichleRepository _vehicleRepository;
        private readonly ICustomerRespository _customerRepo;
        private readonly IVehicleService _vehicleService;
        private readonly CustomerHelper _customerHelper;

        public VehicleController(IVehichleRepository vehicleRepository, ICustomerRespository customerRespository, IVehicleService vehicleService, CustomerHelper customerHelper)
        {
            _vehicleRepository = vehicleRepository;
            _customerRepo = customerRespository;
            _vehicleService = vehicleService;
            _customerHelper = customerHelper;
        }
        [Authorize(Roles = "Customer,Staff")]
        [HttpPost("add")]
        public async Task<IActionResult> AddVehicle([FromBody] CreateVehicleDto dto)
        {
            try
            {
                int userId = ClaimsHelper.GetUserId(User);

                var customerId = await _customerRepo.GetCustomerIdByUserIdAsync(userId);


                if (customerId == null || customerId == 0)
                    return BadRequest(ApiResponse<string>.FailResponse(
                        "Customer record not found for this user"
                    ));

                var newVehicleId = await _vehicleRepository.AddVehicleAsync(
                    customerId.Value,
                    userId,
                    dto
                );

                return Ok(ApiResponse<int>.SuccessResponse(
                    newVehicleId,
                    "Vehicle added successfully"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailResponse(ex.Message, 500));
            }
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _vehicleService.GetVehicleById(id);
            return StatusCode(data.StatusCode, data);
        }
        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _vehicleService.GetAllVehicles();
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Admin,Staff,Customer")]
        [HttpGet("customer")]
        public async Task<IActionResult> GetVehiclesByCustomerId()
        {
            int userId = ClaimsHelper.GetUserId(User);
            int customerId = await _customerHelper.GetCustomerIdAsync(userId);
            var response = await _vehicleService.GetByCustomerId(customerId);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehichle(int id)
        {
            int userId = ClaimsHelper.GetUserId(User);

            var response = await _vehicleService.DeleteVehichleAsync(id, deletedBy: userId);

            return StatusCode(response.StatusCode, response);
        }
    }
}
