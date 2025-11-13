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
        public async Task<int> AddVehicleAsync(int userId, int customerId, CreateVehicleDto dto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FLAG", "INSERT");
            parameters.Add("@CUSTOMERID", customerId);
            parameters.Add("@VEHICLENUMBER", dto.VehicleNumber);
            parameters.Add("@MODEL", dto.Model);
            parameters.Add("@YEAR", dto.Year);
            parameters.Add("@DESCRIPTION", dto.Description);
            parameters.Add("@BRAND", dto.Brand);
            parameters.Add("@CREATEDBY", userId);
            string fuelTypeValue = dto.FuelType.ToString();
            parameters.Add("@FUELTYPE", fuelTypeValue);

            var newVehichleId = await _db.ExecuteScalarAsync<int>(
            "SP_VEHICHLES", parameters, commandType: CommandType.StoredProcedure
            );
            return newVehichleId;
        }
        public Task<int> UpdateVehicleAsync(int userId, UpdateVehicleDto dto) => Task.FromResult(0);
        public Task<int> DeleteVehicleAsync(int vehicleId, int userId) => Task.FromResult(0);
        public Task<IEnumerable<VehicleResponseDto>> GetAllVehiclesAsync() => Task.FromResult<IEnumerable<VehicleResponseDto>>(null);
        public Task<VehicleResponseDto> GetVehicleByIdAsync(int vehicleId) => Task.FromResult<VehicleResponseDto>(null);
        public Task<IEnumerable<VehicleResponseDto>> GetVehiclesByCustomerAsync(int customerId) => Task.FromResult<IEnumerable<VehicleResponseDto>>(null);
    }

}
