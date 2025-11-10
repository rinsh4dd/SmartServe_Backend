using System.Text.Json.Serialization;

public class UpdateStaffDto
{
    public int StaffId { get; set; }
    [JsonIgnore]
    public int? UserId { get; set; }
    public int? DepartmentId { get; set; }
    public decimal? Salary { get; set; }
    public string? Remarks { get; set; }
    public bool? IsActive { get; set; }
    [JsonIgnore]
    public int ModifiedBy { get; set; }
}
