using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

public class AppointmentsRepository : IAppointmentsRepository
{
    private readonly IDbConnection _db;

    public AppointmentsRepository(IDbConnection db)
    {
        _db = db;
    }
    public async Task<IEnumerable<dynamic>> GetWindowsAsync(DateTime date)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETWINDOWS");
        p.Add("@APPOINTMENTDATE", date);

        var result = await _db.QueryAsync<dynamic>(
            "SP_APPOINTMENTS",
            p,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }
    public async Task<int> CreateAppointmentAsync(CreateAppointmentDto dto, int createdBy, int customerId)
    {
        try
        {
            var p = new DynamicParameters();
            p.Add("@FLAG", "BOOK");
            p.Add("@CUSTOMERID", customerId);
            p.Add("@VEHICLEID", dto.VehicleId);
            p.Add("@SERVICETYPE", dto.ServiceType);
            p.Add("@APPOINTMENTDATE", dto.AppointmentDate);
            p.Add("@WINDOWID", dto.WindowId);
            p.Add("@PROBLEMDESCRIPTION", dto.ProblemDescription);
            p.Add("@CREATEDBY", createdBy);

            return await _db.QueryFirstOrDefaultAsync<int>(
                "SP_APPOINTMENTS",
                p,
                commandType: CommandType.StoredProcedure
            );
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }



    public async Task<int> AssignTechAsync(int appointmentId, int technicianId, int staffUserId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "ASSIGNTECH");
        p.Add("@APPOINTMENTID", appointmentId);
        p.Add("@TECHNICIANID", technicianId);
        p.Add("@MODIFIEDBY", staffUserId);

        return await _db.QueryFirstOrDefaultAsync<int>(
             "SP_APPOINTMENTS",
             p,
             commandType: CommandType.StoredProcedure
         );
    }


    public async Task<dynamic> GetByIdAsync(int appointmentId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETBYID");
        p.Add("@APPOINTMENTID", appointmentId);

        return await _db.QueryFirstOrDefaultAsync<dynamic>(
            "SP_APPOINTMENTS",
            p,
            commandType: CommandType.StoredProcedure
        );


    }

    public async Task<IEnumerable<dynamic>> GetAllAsync()
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETALL");

        var result = await _db.QueryAsync<dynamic>(
            "SP_APPOINTMENTS",
            p,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }

    public async Task<int> CancelAsync(int appointmentId, int modifiedBy, int deletedBy)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "CANCEL");
        p.Add("@APPOINTMENTID", appointmentId);
        p.Add("@MODIFIEDBY", modifiedBy);
        p.Add("@DELETEDBY", deletedBy);

        return await _db.QueryFirstOrDefaultAsync<int>(
            "SP_APPOINTMENTS",
            p,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<dynamic>> GetAppointmentHistoryAsync(int customerId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GET_HISTORY_BY_CUSTOMER");
        p.Add("@CUSTOMERID", customerId);

        return await _db.QueryAsync<dynamic>(
            "SP_APPOINTMENTS",
            p,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<bool> DeleteAsync(int appointmentId, int deletedBy)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "DELETE");
        p.Add("@APPOINTMENTID", appointmentId);
        p.Add("@DELETEDBY", deletedBy);

        var rows = await _db.ExecuteAsync(
            "SP_APPOINTMENTS",
            p,
            commandType: CommandType.StoredProcedure
        );

        return rows > 0;
    }
    public async Task<dynamic?> GetExistingAppointmentAsync(int vehicleId, DateTime date, int windowId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETALL");
        var all = await _db.QueryAsync<dynamic>("SP_APPOINTMENTS", p, commandType: CommandType.StoredProcedure);

        return all.FirstOrDefault(x =>
            x.VehicleId == vehicleId &&
            x.AppointmentDate == date &&
            x.WindowId == windowId &&
            x.IsDeleted == 0
        );
    }

    public async Task<bool> CheckTechnicianExistsAsync(int technicianId)
    {
        var sql = @"SELECT COUNT(1) 
                FROM Technicians 
                WHERE TechnicianId = @TechId
                  AND IsDeleted = 0";

        var count = await _db.ExecuteScalarAsync<int>(sql, new { TechId = technicianId });

        return count > 0;
    }

}


