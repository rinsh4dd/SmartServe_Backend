using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Helpers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Technician,Staff")]
public class ServiceJobsController : ControllerBase
{
    private readonly IServiceJobService _service;
    public ServiceJobsController(IServiceJobService service) => _service = service;

    [HttpPost("create-if-not-exists")]
    public async Task<IActionResult> CreateIfNotExists([FromBody] CreateJobDto dto)
    {
        int userId = ClaimsHelper.GetUserId(User);
        var res = await _service.CreateJobIfNotExistsAsync(dto.AppointmentId, dto.TechnicianId, userId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("{id}/start")]
    public async Task<IActionResult> Start(int id)
    {
        int userId = ClaimsHelper.GetUserId(User);
        var res = await _service.StartJobAsync(id, userId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("{id}/add-product")]
    public async Task<IActionResult> AddProduct(int id, AddProductDto dto)
    {
        int userId = ClaimsHelper.GetUserId(User);
        var res = await _service.AddProductToJobAsync(id, dto.AppointmentId, dto.ProductId, dto.Quantity, dto.TechnicianId, userId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(int id, [FromBody] CompleteJobDto dto)
    {
        int userId = ClaimsHelper.GetUserId(User);
        var res = await _service.CompleteJobAsync(id, userId, dto.WorkDescription);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        int userId = ClaimsHelper.GetUserId(User);
        var res = await _service.GetJobAsync(id, userId);
        return StatusCode(res.StatusCode, res);
    }
}
