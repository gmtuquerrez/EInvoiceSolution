namespace EInvoice.Infrastructure.Domain.Entities
{
    public class InvoiceItemTax
    {
        public long Id { get; set; }
        public long InvoiceItemId { get; set; }
        public InvoiceItem? InvoiceItem { get; set; }

        public string TaxCode { get; set; } = null!;
        public string PercentageCode { get; set; } = null!;
        public decimal? Rate { get; set; }
        public decimal TaxableBase { get; set; }
        public decimal Value { get; set; }
    }
}
