using SmartServe.Application.Common;
using SmartServe.Application.Contracts.Repository;
using SmartServe.Application.Contracts.Services;
using SmartServe.Application.DTOs;
using SmartServe.Application.DTOs.AuthDto;
using SmartServe.Application.DTOs.UserDto;
using SmartServe.Domain.Enums;

namespace SmartServe.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly ITechnicianRepository _technicianRepo;
        private readonly IEmailService _emailService;

        public UserService  (
            IUserRepository repo,
            ITechnicianRepository technicianRepo,
            IEmailService emailService)
        {
            _repo = repo;
            _technicianRepo = technicianRepo;
            _emailService = emailService;
        }

        public async Task<ApiResponse<int>> CreateUserAsync(CreateUserDto dto)
        {
            var tempPassword = Guid.NewGuid().ToString("N")[..8];
            var hashed = BCrypt.Net.BCrypt.HashPassword(tempPassword);

            var userId = await _repo.CreateUserAsync(dto, hashed);

            if (dto.Role == Roles.Technician)
            {
                await _technicianRepo.CreateTechnicianAsync(userId, dto.CreatedBy);

                await _emailService.SendEmailAsync(
                    dto.UserEmail,
                    "Your SmartServe Technician Account",
                    $@"Hello {dto.UserName},<br><br>
                       Your technician account has been created.<br>
                       <b>Temporary Password:</b> {tempPassword}<br><br>
                       Login and update your password immediately."
                );

                return new ApiResponse<int>(
                    200,
                    $"User created successfully. Credentials sent to {dto.UserEmail}.",
                    userId
                );
            }

            return new ApiResponse<int>(200, "User created successfully.", userId);
        }

        public async Task<ApiResponse<IEnumerable<UserResponseDto>>> GetAllAsync()
        {
            var users = await _repo.GetAllUsersAsync();

            return new ApiResponse<IEnumerable<UserResponseDto>>(
                200,
                "Users fetched successfully.",
                users
            );
        }

        public async Task<ApiResponse<UpdateUserDto>> UpdateUserAsync(UpdateUserDto dto)
        {
            var existingUser = await _repo.GetUserByIdAsync(dto.UserId);

            if (existingUser == null || existingUser.IsDeleted)
                return new ApiResponse<UpdateUserDto>(404, "User not found.");

            await _repo.UpdateUserAsync(dto);

            return new ApiResponse<UpdateUserDto>(200, "User updated successfully.", dto);
        }

        public async Task<ApiResponse<object>> GetUserByIdAsync(int userId)
        {
            var user = await _repo.GetUserByIdAsync(userId);

            if (user == null)
                return new ApiResponse<object>(404, "User not found.");

            return new ApiResponse<object>(200, "Success", user);
        }

        public async Task<ApiResponse<DeleteUserDto>> DeleteUserAsync(DeleteUserDto dto)
        {
            var existing = await _repo.GetUserByIdAsync(dto.UserId);

            if (existing == null)
                return new ApiResponse<DeleteUserDto>(404, "User not found.");

            await _repo.DeleteUserAsync(dto);

            return new ApiResponse<DeleteUserDto>(200, "User deleted successfully.");
        }
        public async Task<ApiResponse<string>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var currentHash = await _repo.GetPasswordHashAsync(dto.UserId);

            if (currentHash == null)
                return new ApiResponse<string>(404, "User not found.");
            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, currentHash))
                return new ApiResponse<string>(400, "Current password is incorrect.");

            var newHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            await _repo.ChangePasswordAsync(dto.UserId, newHash);

            return new ApiResponse<string>(200, "Password changed successfully.");
        }

        public async Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            string otp = new Random().Next(100000, 999999).ToString();

            DateTime expiry = DateTime.UtcNow.AddMinutes(10);

            var affected = await _repo.SaveResetOtpAsync(dto.Email, otp, expiry);
            if (affected == 0)
            {
                return new ApiResponse<string>(404, "no user found with this email");
            }
            await _emailService.SendEmailAsync(
                dto.Email,
                "SmartServe Password Reset",
                $"Your OTP is <b>{otp}</b>. It expires in 10 minutes."
            );

            return new ApiResponse<string>(200, "OTP sent to email.");
        }

        public async Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var (otp, expiry) = await _repo.GetResetOtpAsync(dto.Email);

            if (otp == null)
                return new ApiResponse<string>(404, "OTP request not found for this email.");

            if (expiry < DateTime.UtcNow)
                return new ApiResponse<string>(400, "OTP has expired.");

            if (otp != dto.Otp)
                return new ApiResponse<string>(400, "Incorrect OTP.");

            string newHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            await _repo.ResetPasswordAsync(dto.Email, newHash);

            return new ApiResponse<string>(200, "Password reset successful.");
        }

    }
}
