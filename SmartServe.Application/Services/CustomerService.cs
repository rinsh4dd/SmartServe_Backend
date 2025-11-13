using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartServe.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRespository _customerRepo;

        public CustomerService(ICustomerRespository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<ApiResponse<IEnumerable<dynamic>>> GetAllCustomersAsync()
        {
            var data = await _customerRepo.GetAllCustomers();

            if (data == null || !data.Any())
            {
                return new ApiResponse<IEnumerable<dynamic>>(404, "Customers Not Found");
            }

            return new ApiResponse<IEnumerable<dynamic>>(200, "Customers retrieved successfully", data);
        }
    }
}
