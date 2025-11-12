using System.Text.Json.Serialization;
using SmartServe.Domain.Enums;

public class UpdateUserDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public Roles Role { get; set; }
    public bool? IsActive { get; set; }

    [JsonIgnore]
    public int ModifiedBy { get; set; }
}
