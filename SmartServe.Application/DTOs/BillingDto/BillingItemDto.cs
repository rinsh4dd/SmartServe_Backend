public class BillingItemDto
{
    public int BillingItemId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal LabourCharge { get; set; }
    public decimal Total { get; set; }

    public DateTime CreatedOn { get; set; }
}
