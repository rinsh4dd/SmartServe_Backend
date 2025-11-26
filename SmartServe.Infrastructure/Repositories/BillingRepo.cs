using System.Data;
using Dapper;
using SmartServe.Application.Contracts.Repository;
namespace SmartServe.Infrastructure.Repositorie
{
    public class BillingRepository : IBillingRepository
    {
        private readonly IDbConnection _db;
        public BillingRepository(IDbConnection db)
        {
            _db = db;
        }
        public async Task<BillingSummaryDto> GetBillingSummaryAsync(int billingId)
        {
            return await _db.QueryFirstOrDefaultAsync<BillingSummaryDto>(
                "SP_BILLING",
                new { FLAG = "GET_SUMMARY", BillingId = billingId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<BillingItemDto>> GetBillingItemsAsync(int billingId)
        {
            return await _db.QueryAsync<BillingItemDto>(
                "SP_BILLING",
                new { FLAG = "GET_ITEMS", BillingId = billingId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<BillingListDto>> GetAllBillsAsync()
        {
            return await _db.QueryAsync<BillingListDto>(
                "SP_BILLING",
                new { FLAG = "GET_ALL" },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<BillingListDto>> GetBillsByCustomerAsync(int customerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GET_BY_CUSTOMER");
            parameters.Add("@CustomerId", customerId);

            return await _db.QueryAsync<BillingListDto>(
                "SP_BILLING",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

    }
}

