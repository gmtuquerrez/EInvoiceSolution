using EInvoice.Infrastructure.Domain.Entities;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
using EInvoiceSolution.Core.Invoices.Models.Filters;
using EInvoiceSolution.Core.Shared;

namespace EInvoice.Infrastructure.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateAsync(Invoice invoice);

        Task<PagedResult<InvoiceHeaderDto>> GetInvoicesByCriteriaAsync(InvoiceCriteria criteria);
    }
}
