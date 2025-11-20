using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
using EInvoiceSolution.Core.Invoices.Models.Response;
using EInvoiceSolution.Core.Shared;

namespace EInvoice.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<OperationalResult<InvoiceCreatedResponse>> CreateInvoiceAsync(InvoiceModel model, string createdBy);

        Task<OperationalResult<List<InvoiceHeaderDto>>> GetInvoicesByCriteriaAsync(int? statusId = null, long? companyId = null, string? ruc = null, int? daysBack = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
