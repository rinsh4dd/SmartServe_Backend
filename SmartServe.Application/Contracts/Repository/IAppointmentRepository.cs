public interface IAppointmentsRepository
{
    Task<IEnumerable<dynamic>> GetWindowsAsync(DateTime date);
    Task<int> CreateAppointmentAsync(CreateAppointmentDto dto, int createdBy, int customerId);
    Task<int> AssignTechAsync(int appointmentId, int technicianId, int staffUserId);
    Task<dynamic> GetByIdAsync(int appointmentId);
    Task<IEnumerable<dynamic>> GetAllAsync();
    Task<int> CancelAsync(int appointmentId, int modifiedBy, int deletedBy);
    Task<bool> DeleteAsync(int appointmentId, int deletedBy);
    Task<dynamic?> GetExistingAppointmentAsync(int vehicleId, DateTime date, int windowId);
    Task<bool> CheckTechnicianExistsAsync(int technicianId);
    Task<IEnumerable<dynamic>> GetAppointmentHistoryAsync(int customerId);

}

