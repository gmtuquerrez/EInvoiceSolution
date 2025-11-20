using EInvoice.Infrastructure.Domain.Entities;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services.Contracts;
using EInvoiceSolution.Core.Invoices.Factories;
using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Models.Response;
using Newtonsoft.Json;

namespace EInvoice.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmissionPointRepository _emissionPointRepository;
        private readonly ICompanyRepository _companyRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository,
            ICustomerRepository customerRepository,
            IEmissionPointRepository emissionPointRepository,
            ICompanyRepository companyRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _emissionPointRepository = emissionPointRepository;
            _companyRepository = companyRepository;
        }

        public async Task<InvoiceCreatedResponse> CreateInvoiceAsync(InvoiceModel model, string createdBy)
        {
            // Validar customer
            var customer = await _customerRepository.GetByIdentificationAsync(model.CustomerIdentification);

            if (customer == null)
                throw new Exception("Customer not found.");

            // Validar company
            var company = await _companyRepository.GetByRucAsync(model.Ruc);
            if (company == null)
                throw new Exception("Company not found.");

            // Validar punto de emision
            var emissionPoint = await _emissionPointRepository.GetByCodeAndCompanyAsync(model.Establishment, company.Id);
            if (emissionPoint == null)
                throw new Exception("EmissionPoint not found.");

            // Mapeo externo (factory)
            var invoice = InvoiceFactory.CreateEntity(
                model,
                customer.Id,
                company.Id,
                emissionPoint.Id
            );

            invoice.CreatedBy = createdBy;

            // 2. Guardamos en base
            var created = await _invoiceRepository.CreateAsync(invoice);

            // 3. Response limpio
            return new InvoiceCreatedResponse
            {
                InvoiceId = created.Id,
                AccessKey = created.AccessKey,
                Status = "CREATED"
            };
        }
    }
}
