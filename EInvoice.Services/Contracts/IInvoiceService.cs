using EInvoice.Infrastructure.Domain.Entities;

namespace EInvoice.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    }
}
