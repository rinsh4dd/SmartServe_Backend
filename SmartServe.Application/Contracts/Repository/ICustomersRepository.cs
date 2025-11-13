namespace SmartServe.Application.Contracts.Repository
{
    public interface ICustomerRespository
    {
        Task<int> AddCustomerForUserAsync(int userId, string email, string name);
        Task<int> GetCustomerIdByUserIdAsync(int userId);
        Task<IEnumerable<dynamic>> GetAllCustomers();
    }
}