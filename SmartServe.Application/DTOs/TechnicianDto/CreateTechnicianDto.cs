using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartServe.Application.DTOs.TechnicianDto
{
    public class CreateTechnicianDto
    {
        public int UserId { get; set; }

        public string? Experience { get; set; }

        public bool? IsAvailable { get; set; }

        public decimal? Salary { get; set; }

        public DateTime? JoinedDate { get; set; }

        public string? Bio { get; set; }
        public string? Status { get; set; }
    }
}

