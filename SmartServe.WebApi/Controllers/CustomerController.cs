using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Services;


namespace SmartServe.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : ControllerBase
    {
        public readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customerService.GetAllCustomersAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}

