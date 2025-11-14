namespace EInvoiceSolution.Core.Invoices.Models
{
    public class TotalWithTaxesModel
    {
        public string TaxCode { get; set; }              // codigo
        public string PercentageCode { get; set; }       // codigoPorcentaje
        public decimal TaxableBase { get; set; }         // baseImponible
        public decimal Value { get; set; }               // valor
    }
}
