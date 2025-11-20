using EInvoice.Infrastructure.Factories;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services.Contracts;
using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
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
            // Validar customer
            var customer = await _customerRepository.GetByIdentificationAsync(model.CustomerIdentification);
            if (customer == null)
                return OperationalResult<InvoiceCreatedResponse>.Fail("Customer not found.");

            // Validar company
            var company = await _companyRepository.GetByRucAsync(model.Ruc);
            if (company == null)
                return OperationalResult<InvoiceCreatedResponse>.Fail("Company not found.");

            // Validar punto de emisión
            var emissionPoint = await _emissionPointRepository.GetByCodeAndCompanyAsync(model.Establishment, company.Id);
            if (emissionPoint == null)
                return OperationalResult<InvoiceCreatedResponse>.Fail("EmissionPoint not found.");

            // Mapeo usando factory
            var invoice = InvoiceFactory.CreateEntity(model, customer.Id, company.Id, emissionPoint.Id);
            invoice.CreatedBy = createdBy;

            // Guardar en base
            var created = await _invoiceRepository.CreateAsync(invoice);

            // Retornar resultado exitoso
            var response = new InvoiceCreatedResponse
            {
                InvoiceId = created.Id,
                AccessKey = created.AccessKey,
                Status = "CREATED"
            };

            return OperationalResult<InvoiceCreatedResponse>.Ok(response);
        }

        public async Task<OperationalResult<List<InvoiceHeaderDto>>> GetInvoicesByCriteriaAsync(int? statusId = null, long? companyId = null, string? ruc = null, int? daysBack = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var query = _invoiceRepository.Query(); // IQueryable<Invoice>

                if (statusId.HasValue)
                    query = query.Where(i => i.StatusId == statusId.Value);

                if (companyId.HasValue)
                    query = query.Where(i => i.CompanyId == companyId.Value);

                if (!string.IsNullOrEmpty(ruc))
                    query = query.Where(i => i.Ruc == ruc);

                if (daysBack.HasValue)
                {
                    var dateFrom = DateTime.UtcNow.AddDays(-daysBack.Value);
                    query = query.Where(i => i.IssueDate >= dateFrom);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(i => i.IssueDate >= startDate.Value && i.IssueDate <= endDate.Value);
                }

                var invoices = await query
                                    .AsNoTracking()
                                    .Select(i => i.ToHeaderDto())
                                    .ToListAsync();

                return OperationalResult<List<InvoiceHeaderDto>>.Ok(invoices);
            }
            catch (Exception ex)
            {
                return OperationalResult<List<InvoiceHeaderDto>>.Fail(message: ex.Message);
            }
        }
    }
}
