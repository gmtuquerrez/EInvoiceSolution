using EInvoice.Infrastructure.Domain.Entities;

namespace EInvoice.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdentificationAsync(string identification);
    }

}
