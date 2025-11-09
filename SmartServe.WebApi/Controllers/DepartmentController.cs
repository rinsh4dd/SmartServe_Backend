using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.DepartmentDto;
using SmartServe.Application.Helpers;


namespace SmartServe.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]

    public class DepartmentController : ControllerBase
    {
        public readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService depService)
        {
            _departmentService = depService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto dto)
        {
            dto.CreatedBy = ClaimsHelper.GetUserId(User);

            var Response = await _departmentService.CreateDepartmentAsync(dto);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpGet]   
        public async Task<IActionResult> GetAllDepartments()
        {
            var response = await _departmentService.GetAllDepartments();
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            int deletedBy = ClaimsHelper.GetUserId(User);
            var response = await _departmentService.DeleteDepartmentAsync( departmentId, deletedBy );
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult>GetById(int id)
        {
            var response = await  _departmentService.GetDepartmentById(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDept(UpdateDepartmentDto dto)
        {
            dto.ModifiedBy = ClaimsHelper.GetUserId(User);
            var response =await _departmentService.UpdateDepartmentAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}

