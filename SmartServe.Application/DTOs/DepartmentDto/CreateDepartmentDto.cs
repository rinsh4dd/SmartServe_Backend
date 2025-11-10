using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartServe.Application.DTOs.DepartmentDto
{
    public class CreateDepartmentDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public int CreatedBy { get; set; }
    }
}

