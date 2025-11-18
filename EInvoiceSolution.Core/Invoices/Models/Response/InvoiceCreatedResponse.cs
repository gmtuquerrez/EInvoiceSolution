namespace EInvoiceSolution.Core.Invoices.Models.Response
{
    public class InvoiceCreatedResponse
    {
        public long InvoiceId { get; set; }
        public string AccessKey { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
