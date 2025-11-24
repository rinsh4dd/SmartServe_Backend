public interface IServiceJobRepository
{
    Task<int> StartJobAsync(int serviceJobId, int userId);
    Task<int> AddProductToJobAsync(int jobId, AddProductToJobDto dto, int technicianId, int userId);
    Task<int> CompleteJobAsync(int serviceJobId, int userId, string workDescription = null);
    Task<dynamic> GetJobByIdAsync(int serviceJobId);
}
