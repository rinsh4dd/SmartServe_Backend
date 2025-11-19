
using SmartServe.Application.Common;

namespace SmartServe.Application.Contracts.Services
{
    public interface IAppointmentService
    {
        Task<ApiResponse<int>> CreateAppointmentAsync(CreateAppointmentDto dto, int createdBy);

        // Task<bool> UpdateAppointmentAsync(int appointmentId, UpdateAppointmentDto dto, int modifiedBy);

        // Assigns a technician to an appointment and marks status as Assigned
        // Task<bool> AssignTechnicianAsync(int appointmentId, int technicianId, int assignedBy);

        // Cancels an appointment (records CancelledOn & CancelledBy)
        // Task<bool> CancelAppointmentAsync(int appointmentId, int cancelledBy);

        // Gets appointment details by id
        // Task<AppointmentDetailDto?> GetAppointmentByIdAsync(int appointmentId);

        // Task<IEnumerable<AppointmentListDto>> GetAppointmentsByCustomerAsync(int customerId);

        // Optional: returns available time slots for a given date (useful for frontend)
        // Task<IEnumerable<string>> GetAvailableSlotsAsync(DateTime date);
    }
}
