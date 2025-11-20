using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Domain.Entities;
using EInvoice.Infrastructure.Factories;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
using EInvoiceSolution.Core.Invoices.Models.Filters;
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

        public async Task<List<InvoiceHeaderDto>> GetInvoicesByCriteriaAsync(InvoiceCriteria criteria)
        {
            var query = _context.Invoices
                .Include(i => i.Status)
                .Include(i => i.Company)
                .AsQueryable();

            // --- STATUS (name OR id) ---
            if (!string.IsNullOrWhiteSpace(criteria.StatusName))
            {
                var name = criteria.StatusName.ToLower();
                query = query.Where(i => i.Status.Name.ToLower() == name);
            }
            else if (criteria.StatusId.HasValue)
            {
                query = query.Where(i => i.StatusId == criteria.StatusId.Value);
            }

            // --- COMPANY (name OR id) ---
            if (!string.IsNullOrWhiteSpace(criteria.CompanyName))
            {
                var name = criteria.CompanyName.ToLower();
                query = query.Where(i => i.Company.BusinessName.ToLower() == name);
            }
            else if (criteria.CompanyId.HasValue)
            {
                query = query.Where(i => i.CompanyId == criteria.CompanyId.Value);
            }

            // --- DATE FILTERS ---
            if (criteria.StartDate.HasValue)
            {
                query = query.Where(i => i.CreatedAt >= criteria.StartDate.Value);
            }

            if (criteria.EndDate.HasValue)
            {
                var end = criteria.EndDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(i => i.CreatedAt <= end);
            }

            // --- RETURN DTO ---
            return await query
                .AsNoTracking()
                .Select(i => i.ToHeaderDto())
                .ToListAsync();
        }
    }
}
