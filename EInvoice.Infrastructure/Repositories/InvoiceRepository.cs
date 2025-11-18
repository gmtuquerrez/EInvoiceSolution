using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Domain.Entities;

namespace EInvoice.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly EInvoiceDbContext _context;

        public InvoiceRepository(EInvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }
    }
}
