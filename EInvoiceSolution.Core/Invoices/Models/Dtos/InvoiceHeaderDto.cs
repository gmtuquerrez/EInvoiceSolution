namespace EInvoiceSolution.Core.Invoices.Models.Dtos
{
    public class InvoiceHeaderDto
    {
        public long Id { get; set; }

        public string AccessKey { get; set; } = null!;
        public string DocumentCode { get; set; } = null!;
        public string EstablishmentCode { get; set; } = null!;
        public string EmissionPointCode { get; set; } = null!;
        public string Sequential { get; set; } = null!;

        public DateTime IssueDate { get; set; }

        public string Ruc { get; set; } = null!;
        public decimal TotalAmount { get; set; }

        public long CustomerId { get; set; }
        public long CompanyId { get; set; }
        public long EmissionPointId { get; set; }

        public string JsonData { get; set; } = null!;
        public long StatusId { get; set; }

        // Campos necesarios para firma y consulta al SRI
        public string? XmlGenerated { get; set; }
        public string? XmlSigned { get; set; }
        public string? XmlAuthorized { get; set; }
        public string? SriResponse { get; set; }
    }
}
