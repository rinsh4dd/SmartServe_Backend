using System.Data;
using SmartServe.Application.Models;

namespace SmartServe.Application.Contracts.Repository
{
    public interface IAIRepository
    {
        Task<VehicleAIDataModel> GetVehicleAIDataAsync(int vehicleId);
    }
}
