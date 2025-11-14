namespace EInvoiceSolution.Core.Invoices.Models
{
    public class InvoiceItemModel
    {
        public string Code { get; set; }
        public string AuxCode { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalWiyhoutTaxes { get; set; }
        public List<TaxesModel> Taxes { get; set; } = new List<TaxesModel>();
    }
}
