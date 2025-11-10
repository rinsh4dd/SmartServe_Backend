using Dapper;
using SmartServe.Application.DTOs.DepartmentDto;
using SmartServe.Application.Models.Departments;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Infrastructure.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly IDbConnection _db;
         public DepartmentRepository(IDbConnection db) { 
            _db = db;
        }
        public async Task<int> CreateAsync(CreateDepartmentDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "INSERT");
            parameters.Add("@DEPARTMENTNAME", dto.DepartmentName);
            parameters.Add("@DESCRIPTION", dto.Description);
            parameters.Add("@CREATEDBY", dto.CreatedBy);

            return await _db.ExecuteScalarAsync<int>(
                "SP_DEPARTMENTS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

       public async Task<IEnumerable<DepartmentModel>> GetAllAsync()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG","GETALL");

            return await _db.QueryAsync<DepartmentModel>(
                "SP_DEPARTMENTS",
                parameters,
                commandType: CommandType.StoredProcedure
                );
        }
        public async Task<int> DeleteAsync(int departmentId, int deletedBy)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "DELETE");
            parameters.Add("@DEPARTMENTID", departmentId);
            parameters.Add("@DELETEDBY", deletedBy);

            return await _db.ExecuteScalarAsync<int>(
                "SP_DEPARTMENTS",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<DepartmentModel>GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GETBYID");
            parameters.Add("@DEPARTMENTID", id);

            return await _db.QueryFirstOrDefaultAsync<DepartmentModel>(
              "SP_DEPARTMENTS",
              parameters,
              commandType: CommandType.StoredProcedure
              );

        }

        public async Task<int> UpdateAsync(UpdateDepartmentDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "UPDATE");
            parameters.Add("@DEPARTMENTID", dto.DepartmentId);
            parameters.Add("@DEPARTMENTNAME", dto.DepartmentName);
            parameters.Add("@DESCRIPTION", dto.Description);
            parameters.Add("@MODIFIEDBY", dto.ModifiedBy);
            parameters.Add("@ISACTIVE", dto.IsActive);

            return await _db.ExecuteScalarAsync<int>(
                "SP_DEPARTMENTS", parameters, commandType: CommandType.StoredProcedure
                );
        }

    }

}
