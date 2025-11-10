using Dapper;
using Microsoft.Data.SqlClient;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.Models.Auth;
using System.Data;

namespace SmartServe.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _db;
        public AuthRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> RegisterUserAsync(RegisterUserDto dto)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", "INSERT");
                parameters.Add("@USERNAME", dto.UserName);
                parameters.Add("@USEREMAIL", dto.UserEmail);
                parameters.Add("@PASSWORDHASH", dto.Password);
                parameters.Add("@ROLE", dto.Role.ToString());
                parameters.Add("@CREATEDBY", dto.CreatedBy ?? (object)DBNull.Value, DbType.Int32);
                return await _db.ExecuteScalarAsync<int>(
                    "SP_USERS",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex) when ( ex.Number == 50000)
            {
                return -1;
            }

        }

        public Task<UserModel?> GetUserByEmailAsync(string email)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", "GET_BY_EMAIL");
                parameters.Add("@USEREMAIL", email);
                return _db.QueryFirstOrDefaultAsync<UserModel>(
                    "SP_USERS",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException)
            {
                throw;
            }

        }
    }
}