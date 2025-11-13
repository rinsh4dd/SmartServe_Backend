namespace SmartServe.Application.Contracts.Repository
{
    public interface ICustomerRespository
    {
        Task<int> AddCustomerForUserAsync(int userId, string email, string name);
        Task<dynamic> GetCustomerIdByCustomerIdAsync(int customerId);
        Task<IEnumerable<dynamic>> GetAllCustomers();
    }
}