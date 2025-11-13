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

        public async Task<int> AddCustomerForUserAsync(int userId, string email, string name)
        {
            var param = new DynamicParameters();
            param.Add("@FLAG", "INSERT");
            param.Add("@USERID", userId);
            param.Add("@NAME", name);
            param.Add("@PHONENUMBER", DBNull.Value, DbType.String);
            param.Add("@PROFILEIMAGE", DBNull.Value, DbType.String);
            param.Add("@CREATEDBY", userId);

            return await _db.ExecuteScalarAsync<int>(
                "SP_CUSTOMERS",
                param,
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
    }
}