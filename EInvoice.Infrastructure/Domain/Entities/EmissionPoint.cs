using EInvoice.Infrastructure.Domain.Common;

namespace EInvoice.Infrastructure.Domain.Entities
{
    public class EmissionPoint : AuditableEntity
    {
        public long Id { get; set; }
        public long EstablishmentId { get; set; }
        public string Code { get; set; } = null!; // "001"
        public Establishment? Establishment { get; set; }
    }
}
