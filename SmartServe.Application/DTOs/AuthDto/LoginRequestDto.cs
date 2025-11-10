using System.ComponentModel.DataAnnotations;

namespace SmartServe.Application.DTOs.AuthDto
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
