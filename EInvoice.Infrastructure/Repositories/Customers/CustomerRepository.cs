using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EInvoice.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EInvoiceDbContext _context;

        public CustomerRepository(EInvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdentificationAsync(string identification)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Identification == identification);
        }
    }

}
