using Microsoft.Data.SqlClient;
using SmartServe.Application.Common;

public class ServiceJobService : IServiceJobService
{
    private readonly IServiceJobRepository _repo;
    private readonly IAppointmentsRepository _appointmentsRepo; // for checks
    public ServiceJobService(IServiceJobRepository repo, IAppointmentsRepository appointmentsRepo)
    {
        _repo = repo;
        _appointmentsRepo = appointmentsRepo;
    }

    public async Task<ApiResponse<int>> CreateJobIfNotExistsAsync(int appointmentId, int technicianId, int userId)
    {
        try
        {
            var id = await _repo.CreateJobIfNotExistsAsync(appointmentId, technicianId, userId);
            return new ApiResponse<int>(200, "Service job ready.", id);
        }
        catch (Exception ex) { return new ApiResponse<int>(500, ex.Message); }
    }

    public async Task<ApiResponse<int>> StartJobAsync(int serviceJobId, int userId)
    {
        try
        {
            // Optional: check job belongs to user
            var res = await _repo.StartJobAsync(serviceJobId, userId);
            if (res <= 0) return new ApiResponse<int>(400, "Failed to start job.");
            return new ApiResponse<int>(200, "Job started.", res);
        }
        catch (Exception ex) { return new ApiResponse<int>(400, ex.Message); }
    }

    public async Task<ApiResponse<int>> AddProductToJobAsync(int serviceJobId, int appointmentId, int productId, int qty, int technicianId, int userId)
    {
        try
        {
            if (qty <= 0) return new ApiResponse<int>(400, "Quantity must be > 0.");

            // Optional: validate technician owns job, appointment exists etc
            var jobRaw = await _repo.GetJobByIdAsync(serviceJobId);
            if (jobRaw == null || jobRaw.Job == null) return new ApiResponse<int>(404, "Job not found.");
            int jobTechId = (int)jobRaw.Job.TechnicianId;
            if (jobTechId != technicianId && jobTechId != userId) return new ApiResponse<int>(403, "Not authorized.");

            int insertedId = await _repo.AddProductAsync(serviceJobId, appointmentId, productId, qty, technicianId, userId);
            return new ApiResponse<int>(201, "Product added to job.", insertedId);
        }
        catch (SqlException sqlEx)
        {
            return new ApiResponse<int>(400, sqlEx.Message);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(500, ex.Message);
        }
    }

    public async Task<ApiResponse<int>> CompleteJobAsync(int serviceJobId, int userId, string workDescription = null)
    {
        try
        {
            var jobRaw = await _repo.GetJobByIdAsync(serviceJobId);
            if (jobRaw == null || jobRaw.Job == null) return new ApiResponse<int>(404, "Job not found.");
            int jobTechId = (int)jobRaw.Job.TechnicianId;
            if (jobTechId != userId) return new ApiResponse<int>(403, "Not authorized to complete this job.");

            var billingId = await _repo.CompleteJobAsync(serviceJobId, userId, workDescription);
            if (billingId <= 0) return new ApiResponse<int>(400, "Failed to complete job.");
            return new ApiResponse<int>(200, "Job completed. Billing created.", billingId);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(500, ex.Message);
        }
    }

    public async Task<ApiResponse<dynamic>> GetJobAsync(int serviceJobId, int userId)
    {
        try
        {
            var data = await _repo.GetJobByIdAsync(serviceJobId);
            if (data == null) return new ApiResponse<dynamic>(404, "Job not found.");
            // optionally check authorization
            return new ApiResponse<dynamic>(200, "Success", data);
        }
        catch (Exception ex) { return new ApiResponse<dynamic>(500, ex.Message); }
    }
}
