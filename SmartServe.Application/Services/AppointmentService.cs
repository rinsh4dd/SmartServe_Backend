using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs;
using SmartServe.Domain.Enums;

namespace SmartServe.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentsRepository _repo;
        private readonly IVehichleRepository _vehicleRepo;
        private readonly IStaffRepository _staffRepo;
        private readonly CustomerHelper _customerHelper;

        public AppointmentService(IAppointmentsRepository repo, IVehichleRepository vehichleRepository, IStaffRepository staffRepository, CustomerHelper customerHelper)
        {
            _repo = repo;
            _vehicleRepo = vehichleRepository;
            _staffRepo = staffRepository;
            _customerHelper = customerHelper;
        }

        public async Task<ApiResponse<int>> CreateAppointmentAsync(CreateAppointmentDto dto, int createdBy, int customerId)
        {
            try
            {
                if (dto.AppointmentDate < DateTime.Today)
                    return new ApiResponse<int>(400, "Appointment date cannot be in the past.");

                var windows = await _repo.GetWindowsAsync(dto.AppointmentDate);
                var slot = windows.FirstOrDefault(x => x.WindowId == dto.WindowId);
                var vehichleExisting = await _vehicleRepo.GetVehiclesByCustomerAsync(customerId);
                var existing = await _repo.GetExistingAppointmentAsync(dto.VehicleId, dto.AppointmentDate, dto.WindowId);


                var vehicle = await _vehicleRepo.GetVehicleByIdAsync(dto.VehicleId);

                if (vehicle == null)
                    return new ApiResponse<int>(404, "Vehicle not found.");

                if (vehicle.CustomerId != customerId)
                    return new ApiResponse<int>(400, "This vehicle does not belong to the customer.");

                if (slot == null)
                    return new ApiResponse<int>(404, "Invalid time window.");

                if (slot.Remaining <= 0)
                    return new ApiResponse<int>(400, "Selected time window is full.");

                int newId = await _repo.CreateAppointmentAsync(dto, createdBy, customerId);
                return new ApiResponse<int>(201, "Appointment created successfully.", newId);
            }
            catch (Exception ex)
            {
                var cleanMessage = ex.Message.Trim();
                return new ApiResponse<int>(400, cleanMessage);
            }

        }
        public async Task<ApiResponse<int>> AssignTechnicianAsync(AssignTechDto dto, int staffUserId)
        {
            int staffId = await _staffRepo.GetStaffIdByUserIdAsync(staffUserId);
            if (staffId == 0)
                return new ApiResponse<int>(400, "Only Staff can assign technician");
            int staffDept = await _staffRepo.GetStaffDepartmentAsync(staffId);

            if (staffDept == 0)
                return new ApiResponse<int>(400, "Staff user not found.");

            if (staffDept != 10)
                return new ApiResponse<int>(403, "Only Service Department staff can assign technicians.");

            var appointment = await _repo.GetByIdAsync(dto.AppointmentId);

            if (appointment == null)
                return new ApiResponse<int>(404, "Appointment not found.");
            string statusStr = appointment.Status?.ToString() ?? "Pending";

            if (!Enum.TryParse<AppointmentStatus>(statusStr, out var status))
            {
                status = AppointmentStatus.Pending;
            }


            if (status == AppointmentStatus.Completed)
                return new ApiResponse<int>(400, "Cannot assign technician, appointment already completed.");

            if (status == AppointmentStatus.Cancelled)
                return new ApiResponse<int>(400, "Cannot assign to a cancelled appointment.");

            var techExists = await _repo.CheckTechnicianExistsAsync(dto.TechnicianId);

            if (!techExists)
                return new ApiResponse<int>(404, "Technician not found.");

            var rows = await _repo.AssignTechAsync(dto.AppointmentId, dto.TechnicianId, staffUserId);

            if (rows <= 0)
                return new ApiResponse<int>(400, "Failed to assign technician.");

            return new ApiResponse<int>(200, "Technician assigned successfully.", rows);

        }


        public async Task<ApiResponse<IEnumerable<dynamic>>> GetWindowsAsync(DateTime date)
        {
            try
            {
                if (date < DateTime.Today)
                    return new ApiResponse<IEnumerable<dynamic>>(400, "Cannot check windows for past dates.");

                var windows = await _repo.GetWindowsAsync(date);

                if (windows == null || !windows.Any())
                    return new ApiResponse<IEnumerable<dynamic>>(404, "No windows available for the selected date.");

                return new ApiResponse<IEnumerable<dynamic>>(200, "Fetched windows successfully.", windows);
            }
            catch (Exception ex)
            {
                var clean = ex.Message.Replace("\r", "").Replace("\n", "").Trim();
                return new ApiResponse<IEnumerable<dynamic>>(500, clean);
            }
        }

        public async Task<ApiResponse<dynamic>> GetAppointmentById(int appointmentId)
        {
            var result = await _repo.GetByIdAsync(appointmentId);
            if (result == null)
            {
                return new ApiResponse<dynamic>(404, $"no Appointments Found with id: {appointmentId}");
            }
            return new ApiResponse<dynamic>(200, "Success", result);
        }

        public async Task<ApiResponse<IEnumerable<dynamic>>> GetAllAppointments()   
        {
            var result = await _repo.GetAllAsync();
            if (result == null)
            {
                return new ApiResponse<IEnumerable<dynamic>>(404, "not found");
            }
            return new ApiResponse<IEnumerable<dynamic>>(200, "success", result);
        }

        public async Task<ApiResponse<int>> CancelAppointmentAsync(int appointmentId, int userId)
        {
            try
            {
                var appointment = await _repo.GetByIdAsync(appointmentId);

                if (appointment == null)
                    return new ApiResponse<int>(404, "Appointment not found.");

                string status = appointment.Status?.ToString() ?? string.Empty;

                if (status == "Completed")
                    return new ApiResponse<int>(400, "Cannot cancel a completed appointment.");

                if (status == "Cancelled")
                    return new ApiResponse<int>(400, "Appointment already cancelled.");

                int rows = await _repo.CancelAsync(appointmentId, userId, userId);

                if (rows <= 0)
                    return new ApiResponse<int>(400, "Failed to cancel appointment.");

                return new ApiResponse<int>(200, "Appointment cancelled successfully.", rows);
            }
            catch (Exception ex)
            {
                return new ApiResponse<int>(500, ex.Message.Trim());
            }
        }

        public async Task<ApiResponse<IEnumerable<dynamic>>> GetHistoryAsync(int userId)
        {
            int customerId = await _customerHelper.GetCustomerIdAsync(userId);

            if (customerId == 0)
                return new ApiResponse<IEnumerable<dynamic>>(400, "Customer not found.");

            var history = await _repo.GetAppointmentHistoryAsync(customerId);

            return new ApiResponse<IEnumerable<dynamic>>(200, "Fetched successfully", history);
        }



    }
}
