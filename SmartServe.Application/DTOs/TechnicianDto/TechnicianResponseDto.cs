namespace SmartServe.Application.DTOs.TechnicianDto
{
    public class TechnicianResponseDto
    {
        public int TechnicianId { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        public string? Experience { get; set; }
        public bool IsAvailable { get; set; }
        public decimal? Salary { get; set; }

        public DateTime JoinedDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Bio { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
