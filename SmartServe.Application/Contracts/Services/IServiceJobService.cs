using SmartServe.Application.Common;

public interface IServiceJobService
{
    Task<ApiResponse<int>> CreateJobIfNotExistsAsync(int appointmentId, int technicianId, int userId);
    Task<ApiResponse<int>> StartJobAsync(int serviceJobId, int userId);
    Task<ApiResponse<int>> AddProductToJobAsync(int serviceJobId, int appointmentId, int productId, int qty, int technicianId, int userId);
    Task<ApiResponse<int>> CompleteJobAsync(int serviceJobId, int userId, string workDescription = null);
    Task<ApiResponse<dynamic>> GetJobAsync(int serviceJobId, int userId);
}
