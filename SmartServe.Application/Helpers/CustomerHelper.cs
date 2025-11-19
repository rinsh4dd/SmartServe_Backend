using SmartServe.Application.Contracts.Repository;

public class CustomerHelper
{
    private readonly ICustomerRespository _customerRepo;
    public CustomerHelper(ICustomerRespository customerRepo)
    {
        _customerRepo = customerRepo;
    }

    public async Task<int> GetCustomerIdAsync(int userId)
    {
        var customerId = await _customerRepo.GetCustomerIdByUserIdAsync(userId);

        if (customerId is null)
            throw new Exception("Customer not found for this user.");

        return customerId.Value;
    }
}
