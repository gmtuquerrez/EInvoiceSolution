namespace EInvoiceSolution.Core.Invoices.Models
{
    public class InvoiceModel
    {
        public DateTime IssueDate { get; set; }
        public string Enviroment { get; set; }
        public string EmissionType { get; set; }
        public string CompanyName { get; set; }
        public string TradeMark { get; set; }
        public string Ruc { get; set; }
        public string AccessKey { get; set; }
        public string DocumentCode { get; set; }
        public string Establishment { get; set; }
        public string EmissionPoint { get; set; }
        public string Sequential { get; set; }
        public string EstablishmentAddress { get; set; }
        public string SpecialTaxPayer { get; set; }
        public string RequiredKeepAccounting { get; set; }
        public string BuyerIdentificationType { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalWiyhoutTaxes { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Tip { get; set; }
        public string Currency { get; set; }
        public List<TotalWithTaxesModel> TotalWithTaxes { get; set; } = new List<TotalWithTaxesModel>();
        public CustomerModel Customer { get; set; }
        public List<InvoiceItemModel> Items { get; set; } = new();
    }
}
