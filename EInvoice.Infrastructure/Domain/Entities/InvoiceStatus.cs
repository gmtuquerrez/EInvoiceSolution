using EInvoice.Infrastructure.Domain.Common;

namespace EInvoice.Infrastructure.Domain.Entities
{
    public class InvoiceStatus : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;  // GENERATED, SIGNED ...
        public string? Description { get; set; }
    }
}
