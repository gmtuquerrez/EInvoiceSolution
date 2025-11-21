using EInvoice.Infrastructure.Factories.Invoices;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services.Contracts;
using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
using EInvoiceSolution.Core.Invoices.Models.Filters;
using EInvoiceSolution.Core.Invoices.Models.Response;
using EInvoiceSolution.Core.Shared;

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

        public async Task<OperationalResult<InvoiceCreatedResponse>> CreateInvoiceAsync(InvoiceModel model, string createdBy)
        {
            try
            {
                // TODO: Validate environment create configuration manager service


                // Validate customer
                var customer = await _customerRepository.GetByIdentificationAsync(model.CustomerIdentification);
                if (customer == null)
                    return OperationalResult<InvoiceCreatedResponse>.Fail("Customer not found.");

                // Validate company
                var company = await _companyRepository.GetByRucAsync(model.Ruc);
                if (company == null)
                    return OperationalResult<InvoiceCreatedResponse>.Fail("Company not found.");

                // Validate emission point  
                var emissionPoint = await _emissionPointRepository.GetByCodeAndCompanyAsync(model.Establishment, company.Id);
                if (emissionPoint == null)
                    return OperationalResult<InvoiceCreatedResponse>.Fail("EmissionPoint not found.");

                // Map to entity
                var invoice = InvoiceFactory.CreateEntity(model, customer.Id, company.Id, emissionPoint.Id);
                invoice.CreatedBy = createdBy;

                // Create invoice and save in database
                var created = await _invoiceRepository.CreateAsync(invoice);

                // Return response
                var response = new InvoiceCreatedResponse
                {
                    InvoiceId = created.Id,
                    AccessKey = created.AccessKey,
                    Status = "CREATED"
                };

                return OperationalResult<InvoiceCreatedResponse>.Ok(response);
            }
            catch (Exception ex)
            {

                return OperationalResult<InvoiceCreatedResponse>.Fail(message: ex.Message);
            }
        }

        public async Task<OperationalResult<PagedResult<InvoiceHeaderDto>>> GetInvoicesByCriteriaAsync(InvoiceCriteria criteria)
        {
            try
            {
                var invoices = await _invoiceRepository.GetInvoicesByCriteriaAsync(criteria);

                return OperationalResult<PagedResult<InvoiceHeaderDto>>.Ok(invoices);
            }
            catch (Exception ex)
            {
                return OperationalResult<PagedResult<InvoiceHeaderDto>>.Fail(message: ex.Message);
            }
        }
    }
}
