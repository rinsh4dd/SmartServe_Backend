using SmartServe.Application.Common;

namespace SmartServe.Application.Contracts.Services
{
    public interface ICustomerService
{
    Task<ApiResponse<IEnumerable<dynamic>>> GetAllCustomersAsync();
}
}