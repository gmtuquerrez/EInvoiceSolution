using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EInvoice.Infrastructure.Repositories
{
    public class EmissionPointRepository : IEmissionPointRepository
    {
        private readonly EInvoiceDbContext _context;

        public EmissionPointRepository(EInvoiceDbContext context)
        {
            _context = context;
        }
        public async Task<EmissionPoint?> GetByCodeAndCompanyAsync(string emissionPointCode, long companyId)
        {
            return await _context.EmissionPoints
                        .AsNoTracking()
                        .FirstOrDefaultAsync(ep =>
                            ep.Code == emissionPointCode &&
                            ep.Establishment.CompanyId == companyId);
        }
    }
}
