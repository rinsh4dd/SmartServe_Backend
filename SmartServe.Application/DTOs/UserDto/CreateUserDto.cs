using System.Text.Json.Serialization;
using SmartServe.Domain.Enums;

public class CreateUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;

    public Roles Role { get; set; }
    [JsonIgnore]
    public int CreatedBy { get; set; }
}
