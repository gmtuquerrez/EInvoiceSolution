using EInvoice.Infrastructure.Domain.Common;

namespace EInvoice.Infrastructure.Domain.Entities
{
    public class Establishment : AuditableEntity
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string Code { get; set; } = null!; // "001"
        public string Address { get; set; } = null!;
        public Company? Company { get; set; }
        public ICollection<EmissionPoint> EmissionPoints { get; set; } = new List<EmissionPoint>();
    }
}
