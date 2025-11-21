using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EInvoice.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly EInvoiceDbContext _context;

        public CompanyRepository(EInvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companies!.ToListAsync();
        }

        public async Task<Company?> GetByRucAsync(string ruc)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.Ruc == ruc);
        }
    }
}
