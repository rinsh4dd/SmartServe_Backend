using System.Data;

namespace SmartServe.Application.Contracts.Repository
{
    public interface ICustomerRespository
    {
        Task<int> AddCustomerForUserAsync(int userId, string email, string name, IDbTransaction transaction);
        Task<dynamic> GetCustomerIdByCustomerIdAsync(int customerId);
        Task<IEnumerable<dynamic>> GetAllCustomers();
        Task<int?> GetCustomerIdByUserIdAsync(int userId);

    }
}