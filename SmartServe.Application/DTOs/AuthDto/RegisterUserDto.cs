using SmartServe.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartServe.Application.DTOs.AuthDto
{
    public class RegisterUserDto
    {
     
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6–20 characters")]
        public string Password { get; set; } = string.Empty;
        public Roles Role { get; set; } = Roles.Customer;
        [JsonIgnore]
        public int? CreatedBy { get; set; }
    }
}
