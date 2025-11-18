namespace EInvoice.Infrastructure.Domain.Entities
{
    public class InvoicePayment
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public string Method { get; set; } = null!;
        public decimal Amount { get; set; }
        public int? Term { get; set; }
        public string? TimeUnit { get; set; }
    }
}
