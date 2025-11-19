using EInvoice.Infrastructure.Domain.Common;

namespace EInvoice.Infrastructure.Domain.Entities
{
    public class Invoice : AuditableEntity
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }
        public Company? Company { get; set; }

        public long EmissionPointId { get; set; }
        public EmissionPoint? EmissionPoint { get; set; }

        public string AccessKey { get; set; } = null!;
        public string DocumentCode { get; set; } = null!;
        public string EstablishmentCode { get; set; } = null!;
        public string EmissionPointCode { get; set; } = null!;
        public string Sequential { get; set; } = null!;

        public DateTime IssueDate { get; set; }
        public DateTime? AuthorizationDate { get; set; }

        public string Ruc { get; set; } = null!;
        public decimal TotalAmount { get; set; }

        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public string JsonData { get; set; } = null!;

        public string? XmlGenerated { get; set; }
        public string? XmlSigned { get; set; }
        public string? XmlAuthorized { get; set; }
        public string? SriResponse { get; set; }

        public long StatusId { get; set; }
        public InvoiceStatus? Status { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
        public ICollection<InvoicePayment> Payments { get; set; } = new List<InvoicePayment>();
        public ICollection<InvoiceAdditionalField> AdditionalFields { get; set; } = new List<InvoiceAdditionalField>();
    }
}
