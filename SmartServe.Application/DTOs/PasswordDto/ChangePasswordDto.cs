using System.Text.Json.Serialization;

namespace SmartServe.Application.DTOs.AuthDto
{
    public class ChangePasswordDto
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public string CurrentPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
