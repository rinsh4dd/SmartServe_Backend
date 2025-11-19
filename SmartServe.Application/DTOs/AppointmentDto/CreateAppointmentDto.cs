public class CreateAppointmentDto
{
    public int CustomerId { get; set; }
    public int VehicleId { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public int WindowId { get; set; }    // <-- NEW
    public string? ProblemDescription { get; set; }
}
