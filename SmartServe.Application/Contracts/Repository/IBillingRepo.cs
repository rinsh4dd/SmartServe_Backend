using SmartServe.Application.DTOs;

public interface IBillingRepository
{
    Task<BillingSummaryDto> GetBillingSummaryAsync(int billingId);
    Task<IEnumerable<BillingItemDto>> GetBillingItemsAsync(int billingId);
    Task<IEnumerable<BillingListDto>> GetAllBillsAsync();
    Task<IEnumerable<BillingListDto>> GetBillsByCustomerAsync(int customerId);
}
