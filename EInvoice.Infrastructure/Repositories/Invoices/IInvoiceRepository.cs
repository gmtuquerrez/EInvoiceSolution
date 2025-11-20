using EInvoice.Infrastructure.Domain.Entities;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
using EInvoiceSolution.Core.Invoices.Models.Filters;

namespace EInvoice.Infrastructure.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateAsync(Invoice invoice);

        Task<List<InvoiceHeaderDto>> GetInvoicesByCriteriaAsync(InvoiceCriteria criteria);
    }
}
