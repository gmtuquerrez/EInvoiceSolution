namespace EInvoiceSolution.Core.Invoices.Models
{
    public class TaxesModel
    {
        public string TaxCode { get; set; }
        public string PercentageCode { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxableBase { get; set; }
        public decimal Value { get; set; }

    }
}
