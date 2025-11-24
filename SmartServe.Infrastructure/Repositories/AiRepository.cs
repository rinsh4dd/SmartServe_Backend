using Dapper;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Models;
using System.Data;

public class AIRepository : IAIRepository
{
    private readonly IDbConnection _db;

    public AIRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<VehicleAIDataModel> GetVehicleAIDataAsync(int vehicleId)
    {
        var command = new CommandDefinition(
            "SP_AI_VEHICLE_PROFILE",
            new { VehicleId = vehicleId },
            commandType: CommandType.StoredProcedure,
            commandTimeout: 180 
        );

        using var multi = await _db.QueryMultipleAsync(command);

        return new VehicleAIDataModel
        {
            Vehicle = multi.ReadFirstOrDefault(),
            ServiceJobs = multi.Read().ToList(),
            UsedProducts = multi.Read().ToList(),
            Issues = multi.Read().ToList(),
            ServiceStats = multi.Read().ToList()
        };
    }
}
