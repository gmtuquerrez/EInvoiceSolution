using EInvoice.Infrastructure.Domain.Common;

namespace EInvoice.Infrastructure.Domain.Entities
{
    public class Company : AuditableEntity
    {
        public long Id { get; set; }
        public string Ruc { get; set; } = null!;
        public string BusinessName { get; set; } = null!;
        public string? TradeName { get; set; }
        public string SignatureBase64 { get; set; } = null!;
        public string SignaturePassword { get; set; } = null!;
        public string? LogoBase64 { get; set; }

        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
