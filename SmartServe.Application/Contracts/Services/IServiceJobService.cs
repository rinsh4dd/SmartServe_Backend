using SmartServe.Application.Common;

public interface IServiceJobService
{
    Task<ApiResponse<int>> StartJobAsync(int serviceJobId, int userId);
    Task<ApiResponse<int>> AddProductToJobAsync(int jobId, AddProductToJobDto dto, int technicianId, int userId);
    Task<ApiResponse<int>> CompleteJobAsync(CompleteJobDto dto, int userId);
    Task<ApiResponse<dynamic>> GetJobAsync(int serviceJobId, int userId);
    Task<ApiResponse<IEnumerable<dynamic>>> GetJobsByTechnicianId(int TechnicianId);
}   
