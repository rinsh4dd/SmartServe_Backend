public class CreateJobDto
{
    public int AppointmentId { get; set; }
    public int TechnicianId { get; set; }
}
public class AddProductToJobDto
{
    public int AppointmentId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}


