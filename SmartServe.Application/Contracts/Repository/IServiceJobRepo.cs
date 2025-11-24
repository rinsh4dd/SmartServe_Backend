public interface IServiceJobRepository
{
    Task<int> StartJobAsync(int serviceJobId, int userId);
    Task<int> AddProductToJobAsync(int jobId, AddProductToJobDto dto, int technicianId, int userId);
    Task<int> CompleteJobAsync(CompleteJobDto dto, int userId);
    Task<dynamic> GetJobByIdAsync(int serviceJobId);
    Task<dynamic> GetJobByTechnicianId(int TechnicianId);
}
