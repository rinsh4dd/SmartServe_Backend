using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Helpers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Technician,Staff")]
public class ServiceJobsController : ControllerBase
{
    private readonly IServiceJobService _service;
    private readonly ITechnicianRepository _techncianRepo;
    public ServiceJobsController(IServiceJobService service, ITechnicianRepository technicianRepository)
    {
        _service = service;
        _techncianRepo = technicianRepository;
    }
    [HttpPost("{id}/start")]
    public async Task<IActionResult> Start(int id)
    {
        int userId = ClaimsHelper.GetUserId(User);
        var res = await _service.StartJobAsync(id, userId);
        return StatusCode(res.StatusCode, res);
    }
    [HttpPost("{jobId}/add-product")]
    public async Task<IActionResult> AddProductToJob(int jobId, AddProductToJobDto dto)
    {
        var user = ClaimsHelper.GetUserId(User);
        int technicianId = await _techncianRepo.GetTechnicianIdByUserIdAsync(user);
        if (technicianId <= 0)
            return Unauthorized("Only technicians can add products.");
        var res = await _service.AddProductToJobAsync(jobId, dto, technicianId, user);
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
