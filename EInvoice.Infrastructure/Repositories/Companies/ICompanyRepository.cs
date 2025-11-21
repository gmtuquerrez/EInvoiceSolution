using EInvoice.Infrastructure.Domain.Entities;

namespace EInvoice.Infrastructure.Repositories
{
    public interface ICompanyRepository
    {
        public Task<Company?> GetByRucAsync(string ruc);
        public Task<List<Company>> GetAllAsync();
    }
}
