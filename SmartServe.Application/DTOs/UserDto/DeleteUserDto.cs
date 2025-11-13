using System.Text.Json.Serialization;

namespace SmartServe.Application.DTOs.UserDto
{
    public class DeleteUserDto
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public int DeletedBy { get; set; }
    }
}