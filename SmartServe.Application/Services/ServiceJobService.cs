using Microsoft.Data.SqlClient;
using SmartServe.Application.Common;

public class ServiceJobService : IServiceJobService
{
    private readonly IServiceJobRepository _repo;
    private readonly IAppointmentsRepository _appointmentsRepo;
    public ServiceJobService(IServiceJobRepository repo, IAppointmentsRepository appointmentsRepo)
    {
        _repo = repo;
        _appointmentsRepo = appointmentsRepo;
    }

    public async Task<ApiResponse<int>> StartJobAsync(int serviceJobId, int userId)
    {
        try
        {
            var res = await _repo.StartJobAsync(serviceJobId, userId);
            if (res <= 0) return new ApiResponse<int>(400, "Failed to start job.");
            return new ApiResponse<int>(200, "Job started.", res);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(400, ex.Message);
        }
    }
    public async Task<ApiResponse<int>> AddProductToJobAsync(int jobId, AddProductToJobDto dto, int technicianId, int userId)
    {
        if (dto.Quantity <= 0)
            return new ApiResponse<int>(400, "Quantity must be greater than zero");

        int result = await _repo.AddProductToJobAsync(jobId, dto, technicianId, userId);

        return new ApiResponse<int>(200, "Product added to job successfully", result);
    }
    public async Task<ApiResponse<int>> CompleteJobAsync(CompleteJobDto dto, int userId)
    {
        try
        {
            var jobRaw = await _repo.GetJobByIdAsync(dto.ServiceJobId);
            if (jobRaw == null)
                return new ApiResponse<int>(404, "Job not found.");
            if (dto.LabourCharge <= 0)
                return new ApiResponse<int>(400, "Labour charge is required.");
            var billingId = await _repo.CompleteJobAsync(dto, userId);
            if (billingId <= 0)
                return new ApiResponse<int>(400, "Failed to complete job.");
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
            return new ApiResponse<dynamic>(200, "Success", data);
        }
        catch (Exception ex) { return new ApiResponse<dynamic>(500, ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<dynamic>>> GetJobsByTechnicianId(int TechnicianId)
    {
        try
        {
            var result = await _repo.GetJobByTechnicianId(TechnicianId);
            if (result == null)
            {
                return new ApiResponse<IEnumerable<dynamic>>(404, "no Jobs found");
            }
            return new ApiResponse<IEnumerable<dynamic>>(200, "jobs fetched successfully", result);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<dynamic>>(500, ex.Message);
        }
    }
}
