namespace SmartServe.Application.Contracts.Repository
{
    public interface IAppointmentRepository
    {
        Task<int> CreateAppointmentAsync(CreateAppointmentDto dto, int createdBy);
    }
}
