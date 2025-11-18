using EInvoice.Infrastructure.Domain.Entities;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services.Contracts;

namespace EInvoice.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            // Aquí más adelante agregamos reglas,
            // validación, generación de la clave, etc.
            return _invoiceRepository.CreateAsync(invoice);
        }
    }
}
