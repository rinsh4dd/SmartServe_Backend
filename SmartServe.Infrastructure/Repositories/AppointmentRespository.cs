using Dapper;
using System.Data;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.DTOs;

namespace SmartServe.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDbConnection _db;

        public AppointmentRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateAppointmentAsync(CreateAppointmentDto dto, int createdBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@FLAG", "BOOK");
            parameters.Add("@CUSTOMERID", dto.CustomerId);
            parameters.Add("@VEHICLEID", dto.VehicleId);
            parameters.Add("@SERVICETYPE", dto.ServiceType);
            parameters.Add("@APPOINTMENTDATE", dto.AppointmentDate);
            parameters.Add("@WINDOWID", dto.WindowId);
            parameters.Add("@PROBLEMDESCRIPTION", dto.ProblemDescription);
            parameters.Add("@CREATEDBY", createdBy);

            var id = await _db.QueryFirstOrDefaultAsync<int>(
                "SP_APPOINTMENTS",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return id;
        }
    }
}
