namespace EInvoice.Infrastructure.Domain.Entities
{
    public class InvoiceAdditionalField
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
