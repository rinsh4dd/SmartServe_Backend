using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.DTOs.StaffDto;
using SmartServe.Application.DTOs.UserDto;
using SmartServe.Application.Helpers;
using SmartServe.Domain.Enums;

namespace SmartServe.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IEmailService _emailService;

        public StaffService(IAuthRepository authRepository, IStaffRepository staffRepository, IEmailService emailService)
        {
            _authRepository = authRepository;
            _staffRepository = staffRepository;
            _emailService = emailService;
        }

        public async Task<AuthReponseDto> CreateStaffAsync(CreateStaffDto dto)
        {
            string tempPassword = string.IsNullOrWhiteSpace(dto.Password)
                ? PasswordHelper.GenerateTemporaryPassword()
                : dto.Password;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(tempPassword);

            var userDto = new RegisterUserDto
            {
                UserName = dto.UserName,
                UserEmail = dto.UserEmail,
                Password = hashedPassword,
                Role = Roles.Staff,
                CreatedBy = dto.CreatedBy
            };

            var userId = await _authRepository.RegisterUserAsyncNoTr(userDto);
            if (userId <= 0)
            {
                return new AuthReponseDto(400, "Failed to create Staff user account.");
            }

            var staffId = await _staffRepository.CreateStaffAsync(userId, dto);
            if (staffId <= 0)
            {
                return new AuthReponseDto(400, "Failed to create Staff profile.");
            }

            string subject = "Welcome to SmartServe";
            string body = $@"
        <h3>Hello {dto.UserName},</h3>
        <p>Your SmartServe account has been created successfully.</p>
        <p><b>Email:</b> {dto.UserEmail}<br>
        <b>Temporary Password:</b> {tempPassword}</p>
        <p>Please log in and change your password immediately.</p>
        <p>Best regards,<br>SmartServe Team</p>";

            await _emailService.SendEmailAsync(dto.UserEmail, subject, body);

            return new AuthReponseDto(201, $"Staff created successfully! Credentials sent to {dto.UserEmail}.");
        }

        public async Task<ApiResponse<IEnumerable<object>>> GetAllStaffsAsync()
        {
            var data = await _staffRepository.GetAllAsync();
            if (data == null)
            {
                return new ApiResponse<IEnumerable<object>>(404, "No datas found");
            }
            return new ApiResponse<IEnumerable<object>>(200, "Staffs Fetched Successfully", data);
        }

        public async Task<ApiResponse<dynamic>> GetStaffByIdAsync(int id)
        {
            var data = await _staffRepository.GetByIdAsync(id);
            if (data == null)
            {
                return new ApiResponse<dynamic>(404, "Staff not found");
            }
            return new ApiResponse<dynamic>(200, "Staff fetched Successfully", data);

        }

        public async Task<ApiResponse<int>> UpdateStaffAsync(UpdateStaffDto dto)
        {
            var data = await _staffRepository.UpdateAsync(dto);

            if (data <= 0)
            {
                return new ApiResponse<int>(401, "Failed to Update Staff");
            }
            return new ApiResponse<int>(200, "Staff updated Successfully");
        }

        public async Task<ApiResponse<dynamic>> DeleteStaffAsync(DeleteStaffDto dto)
        {
            var result = await _staffRepository.DeleteAsync(dto);

            if (result == 0)
                return new ApiResponse<dynamic>(404, "Staff not found");

            return new ApiResponse<dynamic>(200, "Staff deleted successfully");
        }

    }
}