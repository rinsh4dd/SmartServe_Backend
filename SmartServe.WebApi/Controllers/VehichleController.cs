using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.DTOs;
using SmartServe.Application.Helpers;
using SmartServe.Application.Contracts.Repository;
using System;
using System.Threading.Tasks;

namespace SmartServe.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehichleRepository _vehicleRepository;
        private readonly ICustomerRespository _customerRepo;

        public VehicleController(IVehichleRepository vehicleRepository, ICustomerRespository customerRespository)
        {
            _vehicleRepository = vehicleRepository;
            _customerRepo = customerRespository;
        }

        // ✅ Add Vehicle
        [HttpPost("add")]
        public async Task<IActionResult> AddVehicle([FromBody] CreateVehicleDto dto)
        {
            try
            {
                int userId = ClaimsHelper.GetUserId(User);

                var realCustomerId = await _customerRepo.GetCustomerIdByUserIdAsync(userId);

                if (realCustomerId == 0)
                    return BadRequest(ApiResponse<string>.FailResponse("Customer record not found for this user"));

                var newVehicleId = await _vehicleRepository.AddVehicleAsync(userId, realCustomerId, dto);

                return Ok(ApiResponse<int>.SuccessResponse(newVehicleId, "Vehicle added successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailResponse(ex.Message, 500));
            }
        }

    }
}
