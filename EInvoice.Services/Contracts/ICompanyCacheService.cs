using EInvoice.Infrastructure.Domain.Entities;

namespace EInvoice.Services.Contracts
{
    public interface ICompanyCacheService
    {
        Task LoadAsync();
        Company? GetById(long id);
        Company? GetByRuc(string ruc);
        IReadOnlyCollection<Company> GetAll();
    }
}
