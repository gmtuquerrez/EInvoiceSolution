using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
using EInvoiceSolution.Core.Invoices.Models.Filters;
using EInvoiceSolution.Core.Invoices.Models.Response;
using EInvoiceSolution.Core.Shared;

namespace EInvoice.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<OperationalResult<InvoiceCreatedResponse>> CreateInvoiceAsync(InvoiceModel model, string createdBy);

        Task<OperationalResult<PagedResult<InvoiceHeaderDto>>> GetInvoicesByCriteriaAsync(InvoiceCriteria criteria);
    }
}
