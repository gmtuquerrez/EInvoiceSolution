using EInvoice.Infrastructure.Domain.Entities;

namespace EInvoice.Infrastructure.Repositories
{
    public interface IEmissionPointRepository
    {
        Task<EmissionPoint?> GetByCodeAndCompanyAsync(string emissionPointCode, long companyId);
    }
}
