using Dapper;
using System.Data;

public class ServiceJobRepository : IServiceJobRepository
{
    private readonly IDbConnection _db;
    public ServiceJobRepository(IDbConnection db) => _db = db;

    public async Task<int> StartJobAsync(int serviceJobId, int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "START");
        p.Add("@ServiceJobId", serviceJobId);
        p.Add("@UserId", userId);
        return await _db.QueryFirstOrDefaultAsync<int>("SP_SERVICEJOBS", p, commandType: CommandType.StoredProcedure);
    }
    public async Task<int> AddProductToJobAsync(int jobId, AddProductToJobDto dto, int technicianId, int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "ADD_PRODUCT");
        p.Add("@ServiceJobId", jobId);
        p.Add("@AppointmentId", dto.AppointmentId);
        p.Add("@ProductId", dto.ProductId);
        p.Add("@Quantity", dto.Quantity);
        p.Add("@TechnicianId", technicianId);
        p.Add("@UserId", userId);

        return await _db.QueryFirstOrDefaultAsync<int>(
            "SP_SERVICEJOBS",
            p,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<int> CompleteJobAsync(CompleteJobDto dto, int userId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "COMPLETE");
        p.Add("@ServiceJobId", dto.ServiceJobId);
        p.Add("@UserId", userId);
        p.Add("@WorkDescription", dto.WorkDescription);
        p.Add("@LabourCharge", dto.LabourCharge);

        return await _db.QueryFirstOrDefaultAsync<int>(
            "SP_SERVICEJOBS",
            p,
            commandType: CommandType.StoredProcedure
        );
    }


    public async Task<dynamic> GetJobByIdAsync(int serviceJobId)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "GET_JOB");
        p.Add("@ServiceJobId", serviceJobId);

        using (var multi = await _db.QueryMultipleAsync("SP_SERVICEJOBS", p, commandType: CommandType.StoredProcedure))
        {
            var job = await multi.ReadFirstOrDefaultAsync();
            var products = (await multi.ReadAsync()).ToList();
            return new { Job = job, Products = products };
        }
    }

    public async Task<dynamic> GetJobByTechnicianId(int TechnicianId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@FLAG", "GET_BY_TECHNICIAN");
        parameters.Add("@TechnicianId", TechnicianId);

        return await _db.QueryAsync(
            "SP_SERVICEJOBS",
            parameters,
            commandType: CommandType.StoredProcedure
        );
    }
}
