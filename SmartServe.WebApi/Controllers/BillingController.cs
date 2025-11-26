using System.Data;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Helpers;

[Route("api/billing")]
[ApiController]
public class BillingController : ControllerBase
{
    private readonly IBillingService _service;
    private readonly IDbConnection _db;
    private readonly ICustomerRespository _customerRepository;


    public BillingController(IBillingService service, IDbConnection db,ICustomerRespository customerRespository)
    {
        _service = service;
        _db = db;
        _customerRepository=customerRespository;
    }
    [HttpGet("{billingId}")]
    [Authorize(Roles = "Admin,Staff")]

    public async Task<IActionResult> GetSummary(int billingId)
    {
        var res = await _service.GetBillingSummaryAsync(billingId);
        return StatusCode(res.StatusCode, res);
    }
    [Authorize(Roles = "Admin,Staff")]
    [HttpGet("{billingId}/items")]
    public async Task<IActionResult> GetItems(int billingId)
    {
        var res = await _service.GetBillingItemsAsync(billingId);
        return StatusCode(res.StatusCode, res);
    }
    [Authorize(Roles = "Admin,Staff")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _service.GetAllBillsAsync();
        return StatusCode(res.StatusCode, res);
    }
    [Authorize(Roles = "Customer")]
    [HttpGet("my-bills")]
    public async Task<IActionResult> GetMyBills()
    {
        int userId = ClaimsHelper.GetUserId(User);
        int? customerId = await _customerRepository.GetCustomerIdByUserIdAsync(userId);
        var res = await _service.GetBillsByCustomerAsync(customerId.Value);
        return StatusCode(res.StatusCode, res);
    }
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        string dbStatus;
        try
        {
            _db.Open();
            _db.Close();
            dbStatus = "Connected";
        }
        catch
        {
            dbStatus = "Disconnected";
        }
        return Ok(new
        {
            app = "Healthy",
            database = dbStatus,
            timestamp = DateTime.UtcNow
        });
    }
}
