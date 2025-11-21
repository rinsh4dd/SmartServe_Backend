
using SmartServe.Application.Common;
using SmartServe.Application.DTOs;

namespace SmartServe.Application.Contracts.Services
{
    public interface IAppointmentService
    {
        Task<ApiResponse<int>> CreateAppointmentAsync(CreateAppointmentDto dto, int createdBy, int customerId);
        Task<ApiResponse<IEnumerable<dynamic>>> GetWindowsAsync(DateTime date);
        Task<ApiResponse<int>> AssignTechnicianAsync(AssignTechDto dto, int staffUserId);
        Task<ApiResponse<dynamic>> GetAppointmentById(int appointmentId);
        Task<ApiResponse<IEnumerable<dynamic>>> GetAllAppointments();
        Task<ApiResponse<int>> CancelAppointmentAsync(int appointmentId, int modifiedBy);
        Task<ApiResponse<IEnumerable<dynamic>>> GetHistoryAsync(int userId);
    }
}
