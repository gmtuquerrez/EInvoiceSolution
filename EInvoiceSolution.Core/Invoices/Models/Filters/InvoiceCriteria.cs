namespace EInvoiceSolution.Core.Invoices.Models.Filters
{
    public class InvoiceCriteria
    {
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }

        public int? StatusId { get; set; }
        public string? StatusName { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


    }
}
