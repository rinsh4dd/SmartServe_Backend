public class BillingListDto
{
    public int BillingId { get; set; }
    public string InvoiceNumber { get; set; }
    public string CustomerName { get; set; }
    public string VehicleNumber { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal GrandTotal { get; set; }

    public int PaymentStatus { get; set; }
    public DateTime CreatedOn { get; set; }
}
