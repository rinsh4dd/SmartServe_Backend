using SmartServe.Application.Common;

namespace SmartServe.Application.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _repo;
        public BillingService(IBillingRepository billingRepository)
        {
            _repo = billingRepository;
        }
        public async Task<ApiResponse<BillingSummaryDto>> GetBillingSummaryAsync(int billingId)
        {
            var data = await _repo.GetBillingSummaryAsync(billingId);
            if (data == null)
                return new ApiResponse<BillingSummaryDto>(404, "Billing not found");

            return new ApiResponse<BillingSummaryDto>(200, "Success", data);
        }

        public async Task<ApiResponse<IEnumerable<BillingItemDto>>> GetBillingItemsAsync(int billingId)
        {
            var data = await _repo.GetBillingItemsAsync(billingId);
            return new ApiResponse<IEnumerable<BillingItemDto>>(200, "Success", data);
        }

        public async Task<ApiResponse<IEnumerable<BillingListDto>>> GetAllBillsAsync()
        {
            var data = await _repo.GetAllBillsAsync();
            return new ApiResponse<IEnumerable<BillingListDto>>(200, "Success", data);
        }
        public async Task<ApiResponse<IEnumerable<BillingListDto>>> GetBillsByCustomerAsync(int userId)
        {
            var data = await _repo.GetBillsByCustomerAsync(userId);
            return new ApiResponse<IEnumerable<BillingListDto>>(200, "Success", data);
        }


    }
}