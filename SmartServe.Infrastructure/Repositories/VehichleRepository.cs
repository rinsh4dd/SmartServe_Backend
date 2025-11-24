using System.Data;
using Dapper;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.DTOs;

namespace SmartServe.Infrastructure.Repositories
{
    public class VehicleRepository : IVehichleRepository
    {
        private readonly IDbConnection _db;
        public VehicleRepository(IDbConnection db)
        {
            _db = db;
        }
        public async Task<int> AddVehicleAsync(int customerId, int createdBy, CreateVehicleDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "INSERT");
            parameters.Add("@CUSTOMERID", customerId);
            parameters.Add("@VEHICLENUMBER", dto.VehicleNumber);
            parameters.Add("@MODEL", dto.Model);
            parameters.Add("@YEAR", dto.Year);
            parameters.Add("@DESCRIPTION", dto.Description);
            parameters.Add("@BRAND", dto.Brand);
            parameters.Add("@CREATEDBY", createdBy);
            parameters.Add("@FUELTYPE", dto.FuelType.ToString());

            return await _db.ExecuteScalarAsync<int>(
                "SP_VEHICHLES",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<VehicleResponseDto> GetVehicleByIdAsync(int vehicleId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GETBYID");
            parameters.Add("@VEHICLEID ", vehicleId);

            return await _db.QueryFirstOrDefaultAsync<VehicleResponseDto>(
                "SP_VEHICHLES",
                parameters,
                commandType: CommandType.StoredProcedure
     );
        }
        public Task<int> UpdateVehicleAsync(int userId, UpdateVehicleDto dto) => Task.FromResult(0);
        public async Task<IEnumerable<VehicleResponseDto>> GetAllVehiclesAsync()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GETALL");

            return await _db.QueryAsync<VehicleResponseDto>(
            "SP_VEHICHLES",
             parameters,
             commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<VehicleResponseDto>> GetVehiclesByCustomerAsync(int customerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "GET_BY_CUSTOMER");
            parameters.Add("CUSTOMERID", customerId);

            return await _db.QueryAsync<VehicleResponseDto>(
            "SP_VEHICHLES",
            parameters,
            commandType: CommandType.StoredProcedure
            );
        }
        public async Task<int> DeleteVehicleAsync(int vehicleId, int deletedBy)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "DELETE");
            parameters.Add("@DELETEDBY", deletedBy);
            parameters.Add("@VEHICLEID", vehicleId);

            return await _db.ExecuteScalarAsync<int>(
            "SP_VEHICHLES",
            parameters,
            commandType: CommandType.StoredProcedure
            );
        }
        public async Task<dynamic> GetVehicleHistoryAsync(int vehicleId)
        {
            var param = new DynamicParameters();
            param.Add("@VehicleId", vehicleId);

            using var multi = await _db.QueryMultipleAsync(
                "SP_VEHICLE_HISTORY",
                param,
                commandType: CommandType.StoredProcedure
            );

            var appointments = (await multi.ReadAsync()).ToList();
            var products = (await multi.ReadAsync()).ToList();

            return new
            {
                Appointments = appointments,
                ProductsUsed = products
            };
        }


    }

}
