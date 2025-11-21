using EInvoice.Infrastructure.Domain.Entities;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services.Contracts;
using System.Collections.Concurrent;

namespace EInvoice.Services
{

    public class CompanyCacheService : ICompanyCacheService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ConcurrentDictionary<long, Company> _companiesById = new();
        private readonly ConcurrentDictionary<string, Company> _companiesByRuc = new();

        public CompanyCacheService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task LoadAsync()
        {
            var companies = await _companyRepository.GetAllAsync();

            _companiesById.Clear();
            _companiesByRuc.Clear();

            foreach (var company in companies)
            {
                _companiesById[company.Id] = company;
                _companiesByRuc[company.Ruc] = company;
            }
        }

        public Company? GetById(long id)
        {
            _companiesById.TryGetValue(id, out var company);
            return company;
        }

        public Company? GetByRuc(string ruc)
        {
            _companiesByRuc.TryGetValue(ruc, out var company);
            return company;
        }

        public IReadOnlyCollection<Company> GetAll() => _companiesById.Values.ToList().AsReadOnly();
    }
}

