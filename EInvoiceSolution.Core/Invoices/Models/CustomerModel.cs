namespace EInvoiceSolution.Core.Invoices.Models
{
    public class CustomerModel
    {
        public string IdentificationType { get; set; } // 04 = RUC, 05 = Cédula
        public string Identification { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
