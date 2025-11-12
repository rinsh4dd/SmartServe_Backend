using Dapper;
using System.Data;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.DTOs.TechnicianDto;

public class TechnicianRepository : ITechnicianRepository
{
    private readonly IDbConnection _db;

    public TechnicianRepository(IDbConnection db)
    {
        _db = db;
    }

    // ✅ CREATE TECHNICIAN (called when user with role = Technician is created)
    public async Task<int> CreateTechnicianAsync(int userId, int createdBy)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "INSERT");
        p.Add("@USERID", userId);
        p.Add("@CREATEDBY", createdBy);

        return await _db.ExecuteScalarAsync<int>(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        );
    }

    // ✅ GET ALL TECHNICIANS
    public async Task<IEnumerable<TechnicianResponseDto>> GetAllAsync()
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETALL");

        return await _db.QueryAsync<TechnicianResponseDto>(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        );
    }

    // ✅ GET BY TECHNICIAN ID
    public async Task<TechnicianResponseDto?> GetByIdAsync(int technicianId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETBYID");
        p.Add("@TECHNICIANID", technicianId);

        return await _db.QueryFirstOrDefaultAsync<TechnicianResponseDto>(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        );
    }

    // ✅ GET BY USER ID (for /me)
    public async Task<TechnicianResponseDto?> GetByUserIdAsync(int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GETBYUSERID");
        p.Add("@USERID", userId);

        return await _db.QueryFirstOrDefaultAsync<TechnicianResponseDto>(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        );
    }

    // ✅ UPDATE PROFILE (Technician updates Experience + Bio)
    public async Task<bool> UpdateProfileAsync(int userId, UpdateTechnicianProfileDto dto, int modifiedBy)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "UPDATE_PROFILE");
        p.Add("@USERID", userId);

        p.Add("@EXPERIENCE", dto.Experience);
        p.Add("@BIO", dto.Bio);

        p.Add("@MODIFIEDBY", modifiedBy);

        return await _db.ExecuteAsync(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        ) > 0;
    }

    // ✅ UPDATE STATUS (Admin)
    public async Task<bool> UpdateStatusAsync(int technicianId, string status, int modifiedBy)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "UPDATE_STATUS");
        p.Add("@TECHNICIANID", technicianId);
        p.Add("@STATUS", status);
        p.Add("@MODIFIEDBY", modifiedBy);

        return await _db.ExecuteAsync(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        ) > 0;
    }

    // ✅ SET AVAILABILITY (Admin / Staff)
    public async Task<bool> SetAvailabilityAsync(int technicianId, bool isAvailable, int modifiedBy)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "SET_AVAILABILITY");
        p.Add("@TECHNICIANID", technicianId);
        p.Add("@ISAVAILABLE", isAvailable);
        p.Add("@MODIFIEDBY", modifiedBy);

        return await _db.ExecuteAsync(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        ) > 0;
    }

    // ✅ SOFT DELETE (Admin)
    public async Task<bool> DeleteAsync(int technicianId, int deletedBy)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "DELETE");
        p.Add("@TECHNICIANID", technicianId);
        p.Add("@DELETEDBY", deletedBy);

        return await _db.ExecuteAsync(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        ) > 0;
    }

    // ✅ RESTORE TECHNICIAN
    public async Task<bool> RestoreAsync(int technicianId, int modifiedBy)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "RESTORE");
        p.Add("@TECHNICIANID", technicianId);
        p.Add("@MODIFIEDBY", modifiedBy);

        return await _db.ExecuteAsync(
            "SP_TECHNICIANS",
            p,
            commandType: CommandType.StoredProcedure
        ) > 0;
    }
}
