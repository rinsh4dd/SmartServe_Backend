public interface IServiceJobRepository
{
    Task<int> CreateJobIfNotExistsAsync(int appointmentId, int technicianId, int userId);
    Task<int> StartJobAsync(int serviceJobId, int userId);
    Task<int> AddProductAsync(int serviceJobId, int appointmentId, int productId, int qty, int technicianId, int userId);
    Task<int> CompleteJobAsync(int serviceJobId, int userId, string workDescription = null);
    Task<dynamic> GetJobByIdAsync(int serviceJobId);
}
