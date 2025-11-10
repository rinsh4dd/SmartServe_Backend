using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartServe.Application.DTOs.DepartmentDto
{
    public class UpdateDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
