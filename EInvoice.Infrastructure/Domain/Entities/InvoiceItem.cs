namespace EInvoice.Infrastructure.Domain.Entities
{
    public class InvoiceItem
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public string Code { get; set; } = null!;
        public string? AuxCode { get; set; }
        public string Description { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalWithoutTaxes { get; set; }

        public ICollection<InvoiceItemTax> Taxes { get; set; } = new List<InvoiceItemTax>();
    }
}
