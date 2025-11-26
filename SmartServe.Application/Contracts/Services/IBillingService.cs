using SmartServe.Application.Common;
using SmartServe.Application.DTOs;

public interface IBillingService
{
    Task<ApiResponse<BillingSummaryDto>> GetBillingSummaryAsync(int billingId);
    Task<ApiResponse<IEnumerable<BillingItemDto>>> GetBillingItemsAsync(int billingId);
    Task<ApiResponse<IEnumerable<BillingListDto>>> GetAllBillsAsync();
    Task<ApiResponse<IEnumerable<BillingListDto>>> GetBillsByCustomerAsync(int userId);
}
