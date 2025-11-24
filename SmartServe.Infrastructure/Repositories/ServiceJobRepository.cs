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

    public async Task<int> CompleteJobAsync(int serviceJobId, int userId, string workDescription = null)
    {
        var p = new DynamicParameters();
        p.Add("@FLAG", "COMPLETE");
        p.Add("@ServiceJobId", serviceJobId);
        p.Add("@UserId", userId);
        p.Add("@WorkDescription", workDescription);
        return await _db.QueryFirstOrDefaultAsync<int>("SP_SERVICEJOBS", p, commandType: CommandType.StoredProcedure);
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
}
