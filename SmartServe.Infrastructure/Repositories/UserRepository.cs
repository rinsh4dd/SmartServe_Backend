using System.Data;
using Dapper;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.DTOs.UserDto;

namespace SmartServe.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateUserAsync(CreateUserDto dto, string passwordHash)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "INSERT");
            parameters.Add("@USERNAME", dto.UserName);
            parameters.Add("@USEREMAIL", dto.UserEmail);
            parameters.Add("@PASSWORDHASH", passwordHash);
            parameters.Add("@ROLE", dto.Role.ToString());
            parameters.Add("@CREATEDBY", dto.CreatedBy);

            return await _db.ExecuteScalarAsync<int>(
                "SP_USERS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GETALL");

            return await _db.QueryAsync<UserResponseDto>(
                "SP_USERS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GETBYID");
            parameters.Add("@USERID", userId);

            return await _db.QueryFirstOrDefaultAsync<UserResponseDto>(
                "SP_USERS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "UPDATE");
            parameters.Add("@USERID", dto.UserId);
            parameters.Add("@USERNAME", dto.UserName);
            parameters.Add("@ROLE", dto.Role.ToString());
            parameters.Add("@ISACTIVE", dto.IsActive);
            parameters.Add("@MODIFIEDBY", dto.ModifiedBy);

            await _db.ExecuteAsync(
                "SP_USERS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeleteUserAsync(DeleteUserDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "DELETE");
            parameters.Add("@USERID", dto.UserId);
            parameters.Add("@DELETEDBY", dto.DeletedBy);

            await _db.ExecuteAsync(
                "SP_USERS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<string?> GetPasswordHashAsync(int userId)
        {
            return await _db.QueryFirstOrDefaultAsync<string>(
                "SELECT PasswordHash FROM Users WHERE UserId = @UserId",
                new { UserId = userId });
        }

        public async Task<bool> ChangePasswordAsync(int userId, string newHash)
        {
            var p = new DynamicParameters();
            p.Add("@UserId", userId);
            p.Add("@PasswordHash", newHash);

            var rows = await _db.ExecuteAsync(
                "SP_CHANGE_PASSWORD",
                p,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }

        public async Task<int> SaveResetOtpAsync(string email, string otp, DateTime expiry)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email);
            p.Add("@ResetOtp", otp);
            p.Add("@OtpExpiry", expiry);

            return await _db.ExecuteScalarAsync<int>(
                "SP_SET_RESET_OTP",
                p,
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<(string? Otp, DateTime? Expiry)> GetResetOtpAsync(string email)
        {
            return await _db.QueryFirstOrDefaultAsync<(string?, DateTime?)>(
                "SELECT ResetOtp, ResetOtpExpiry FROM Users WHERE UserEmail = @Email AND IsDeleted = 0",
                new { Email = email });
        }

        public async Task<bool> ResetPasswordAsync(string email, string newHash)
        {
            var p = new DynamicParameters();
            p.Add("@Email", email);
            p.Add("@NewPasswordHash", newHash);

            var rows = await _db.ExecuteAsync(
                "SP_RESET_PASSWORD",
                p,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }


    }
}
