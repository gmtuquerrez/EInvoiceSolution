using EInvoice.Infrastructure.Domain.Entities;

namespace EInvoice.Infrastructure.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateAsync(Invoice invoice);
    }
}
