using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Models.Response;

namespace EInvoice.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<InvoiceCreatedResponse> CreateInvoiceAsync(InvoiceModel model, string createdBy);
    }
}
