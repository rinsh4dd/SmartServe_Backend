using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartServe.Application.DTOs.StaffDto
{
    public class CreateStaffDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [JsonIgnore]
        public string? Password { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
    }
}
