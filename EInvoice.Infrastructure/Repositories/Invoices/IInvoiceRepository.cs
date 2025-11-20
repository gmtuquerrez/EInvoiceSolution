using EInvoice.Infrastructure.Domain.Entities;
using EInvoiceSolution.Core.Invoices.Models.Dtos;

namespace EInvoice.Infrastructure.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateAsync(Invoice invoice);

        Task<List<InvoiceHeaderDto>> GetInvoicesByCriteriaAsync(int? statusId = null, long? companyId = null, string? ruc = null, DateTime? fromDate = null);
    }
}
