using System.Text.Json.Serialization;

namespace SmartServe.Application.DTOs.StaffDto
{
    public class DeleteStaffDto
    {
        public int StaffId { get; set; }
        [JsonIgnore]
        public int DeletedBy { get; set; }
    }

}