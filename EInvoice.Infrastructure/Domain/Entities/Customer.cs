using EInvoice.Infrastructure.Domain.Common;

namespace EInvoice.Infrastructure.Domain.Entities
{
    public class Customer : AuditableEntity
    {
        public long Id { get; set; }
        public string RucOrId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Email { get; set; }
    }
}
