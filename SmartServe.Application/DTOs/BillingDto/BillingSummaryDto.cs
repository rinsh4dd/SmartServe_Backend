public class BillingSummaryDto
{
    public int BillingId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Phone { get; set; }

    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }

    public string VehicleNumber { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }

    public string InvoiceNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal GrandTotal { get; set; }

    public int PaymentMode { get; set; }  
    public int PaymentStatus { get; set; }

    public DateTime CreatedOn { get; set; }
}
