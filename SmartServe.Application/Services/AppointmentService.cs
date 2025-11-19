using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs;

namespace SmartServe.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<int>> CreateAppointmentAsync(CreateAppointmentDto dto, int createdBy)
        {
            // 🔹 Basic business validations (add more later)
            if (dto.AppointmentDate < DateTime.Today)
            {
                return new ApiResponse<int>(400, "Appointment date cannot be in the past.");
            }

            // 🔹 Save to DB
            int newId = await _repo.CreateAppointmentAsync(dto, createdBy);

            // 🔹 Response with the new Appointment ID
            return new ApiResponse<int>(201, "Appointment created successfully.", newId);
        }
    }
}
