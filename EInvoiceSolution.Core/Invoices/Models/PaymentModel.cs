namespace EInvoiceSolution.Core.Invoices.Models
{
    public class PaymentModel
    {
        public string Method { get; set; }
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public string TimeUnit { get; set; }
    }
}
