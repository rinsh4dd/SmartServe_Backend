using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.Models.Auth;
using System.Data;

namespace SmartServe.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _db;
        private readonly string _connectionString;


        public AuthRepository(IDbConnection db, IConfiguration config)
        {
            _db = db;
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new Exception("Connection string not found.");
        }

        public async Task<int> RegisterUserAsync(RegisterUserDto dto, IDbTransaction transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "INSERT");
            parameters.Add("@USERNAME", dto.UserName);
            parameters.Add("@USEREMAIL", dto.UserEmail);
            parameters.Add("@PASSWORDHASH", dto.Password);
            parameters.Add("@ROLE", dto.Role.ToString());
            parameters.Add("@CREATEDBY", dto.CreatedBy);

            return await transaction.Connection.ExecuteScalarAsync<int>(
                "SP_USERS",
                parameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GET_BY_EMAIL");
            parameters.Add("@USEREMAIL", email);

            var user = await _db.QueryFirstOrDefaultAsync<UserModel>(
                "SP_USERS",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return user;
        }
        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        //non transac

    public async Task<int> RegisterUserAsyncNoTr(RegisterUserDto dto)
{
    using var conn = GetConnection();
    conn.Open();

    var parameters = new DynamicParameters();
    parameters.Add("@FLAG", "INSERT");
    parameters.Add("@USERNAME", dto.UserName);
    parameters.Add("@USEREMAIL", dto.UserEmail);
    parameters.Add("@PASSWORDHASH", dto.Password);
    parameters.Add("@ROLE", dto.Role.ToString());
    parameters.Add("@CREATEDBY", dto.CreatedBy ?? (object)DBNull.Value);

    return await conn.ExecuteScalarAsync<int>(
        "SP_USERS",
        parameters,
        commandType: CommandType.StoredProcedure
    );
}


    }
}
