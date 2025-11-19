using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using SmartServe.Application.Contracts.Repository;

namespace SmartServe.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRespository
    {
        private readonly IDbConnection _db;
        public CustomerRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> AddCustomerForUserAsync(int userId, string email, string name, IDbTransaction transaction)
        {
            var param = new DynamicParameters();
            param.Add("@FLAG", "INSERT");

            param.Add("@USERID", userId);
            param.Add("@NAME", name);

            param.Add("@PHONENUMBER", null, DbType.String);
            param.Add("@ADDRESSID", null, DbType.Int32);
            param.Add("@PROFILEIMAGE", null, DbType.String);

            param.Add("@CREATEDBY", userId);


            return await transaction.Connection.ExecuteScalarAsync<int>(
                "SP_CUSTOMERS",
                param,
                transaction: transaction,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<dynamic> GetCustomerIdByCustomerIdAsync(int customerId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@FLAG", "GETBYID");
            parameters.Add("@CUSTOMERID", customerId);
            return await _db.QueryFirstOrDefaultAsync<dynamic>(
            "SP_CUSTOMERS",
            parameters,
            commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<dynamic>> GetAllCustomers()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GETALL");
            return await _db.QueryAsync<dynamic>(
            "SP_CUSTOMERS",
            parameters,
            commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int?> GetCustomerIdByUserIdAsync(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GET_CUSTOMER_BY_USERID");
            parameters.Add("@USERID", userId);
            return await _db.QueryFirstOrDefaultAsync<int>(
                "SP_CUSTOMERS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

    }
}