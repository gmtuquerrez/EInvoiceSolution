using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Domain.Entities;
using EInvoice.Infrastructure.Factories;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<InvoiceHeaderDto>> GetInvoicesByCriteriaAsync(int? statusId = null, long? companyId = null, string? ruc = null, DateTime? fromDate = null)
        {

            var query = _context.Invoices.AsQueryable();

            if (statusId.HasValue)
                query = query.Where(i => i.StatusId == statusId.Value);

            if (companyId.HasValue)
                query = query.Where(i => i.CompanyId == companyId.Value);

            if (!string.IsNullOrEmpty(ruc))
                query = query.Where(i => i.Ruc == ruc);

            if (fromDate.HasValue)
                query = query.Where(i => i.IssueDate >= fromDate.Value);

            return await query
                .AsNoTracking()
                .Select(i => i.ToHeaderDto())
                .ToListAsync();
        }
    }
}
