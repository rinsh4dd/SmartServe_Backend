using Dapper;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.DTOs.DepartmentDto;
using SmartServe.Application.DTOs.StaffDto;
using System.Data;

namespace SmartServe.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly IDbConnection _db;

        public StaffRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateStaffAsync(int userId, CreateStaffDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "INSERT");
            parameters.Add("@USERID", userId);
            parameters.Add("@DEPARTMENTID", dto.DepartmentId);
            parameters.Add("@JOININGDATE", dto.JoiningDate);
            parameters.Add("@SALARY", dto.Salary);
            parameters.Add("@CREATEDBY", dto.CreatedBy);

            var result = await _db.ExecuteScalarAsync<int>(
                "SP_STAFF",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GETALL");

            return await _db.QueryAsync<object>(
                "SP_STAFF", parameters, commandType: CommandType.StoredProcedure
                );
        }

        public async Task<dynamic> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GETBYID");
            parameters.Add("@STAFFID", id);
            var result = await _db.QueryFirstOrDefaultAsync<dynamic>(
                "SP_STAFF", parameters, commandType: CommandType.StoredProcedure
            );

            return result;

        }

        public async Task<int> UpdateAsync(UpdateStaffDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "UPDATE");
            parameters.Add("@STAFFID", dto.StaffId);
            parameters.Add("@USERID", dto.UserId);
            parameters.Add("@DEPARTMENTID", dto.DepartmentId);
            parameters.Add("@SALARY", dto.Salary);
            parameters.Add("@REMARKS", dto.Remarks);
            parameters.Add("@ISACTIVE", dto.IsActive);
            parameters.Add("@MODIFIEDBY", dto.ModifiedBy);

            var result = await _db.ExecuteScalarAsync<int>(
                "SP_STAFF",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<int> DeleteAsync(DeleteStaffDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "DELETE");
            parameters.Add("@STAFFID", dto.StaffId);
            parameters.Add("@DELETEDBY", dto.DeletedBy);

            var result = await _db.ExecuteScalarAsync<int>(
                "SP_STAFF",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }



    }
}
