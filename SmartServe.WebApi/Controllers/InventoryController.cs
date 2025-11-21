using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.DTOs;
using SmartServe.Application.Helpers;
using SmartServe.Application.Contracts.Repository;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;
    private readonly IStaffRepository _staffRepo;

    public InventoryController(IInventoryService service, IStaffRepository staffRepo)
    {
        _service = service;
        _staffRepo = staffRepo;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddProduct(AddProductDtoStaff dto)
    {
        var user = ClaimsHelper.GetUser(User);

        int staffId = await _staffRepo.GetStaffIdByUserIdAsync(user.UserId);
        int deptId = await _staffRepo.GetDepartmentIdByUserIdAsync(user.UserId);

        var res = await _service.AddProductAsync(dto, user.UserId, user.Role, staffId, deptId);
        return StatusCode(res.StatusCode, res);
    }


    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto dto)
    {
        var user = ClaimsHelper.GetUser(User);

        int staffId = await _staffRepo.GetStaffIdByUserIdAsync(user.UserId);
        int deptId = await _staffRepo.GetDepartmentIdByUserIdAsync(user.UserId);

        var res = await _service.UpdateProductAsync(id, dto, user.UserId, user.Role, staffId, deptId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var user = ClaimsHelper.GetUser(User);
        int staffId = await _staffRepo.GetStaffIdByUserIdAsync(user.UserId);
        int deptId = await _staffRepo.GetDepartmentIdByUserIdAsync(user.UserId);

        var res = await _service.DeleteProductAsync(id, user.UserId, user.Role, staffId,deptId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("stock-in")]
    public async Task<IActionResult> StockIn(int productId, int qty)
    {
        var user = ClaimsHelper.GetUser(User);

        int staffId = await _staffRepo.GetStaffIdByUserIdAsync(user.UserId);
        int deptId = await _staffRepo.GetDepartmentIdByUserIdAsync(user.UserId);

        var res = await _service.StockInAsync(productId, qty, user.UserId, user.Role, staffId, deptId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("stock-out")]
    public async Task<IActionResult> StockOut(int productId, int qty)
    {
        var user = ClaimsHelper.GetUser(User);
        int staffId = await _staffRepo.GetStaffIdByUserIdAsync(user.UserId);

        var res = await _service.StockOutAsync(productId, qty, user.UserId, user.Role, staffId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("value")]
    public async Task<IActionResult> TotalValue()
    {
        var user = ClaimsHelper.GetUser(User);

        if (user.Role != "Admin")
            return Unauthorized("Only Admin can view total inventory value.");

        var res = await _service.GetTotalValueAsync();
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response =await _service.GetAllProductsAsync();
        return StatusCode(response.StatusCode,response);
    }
}
