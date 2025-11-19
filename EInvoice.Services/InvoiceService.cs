using EInvoice.Infrastructure.Domain.Entities;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services.Contracts;
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

            // Mapear InvoiceModel → Entity Invoice
            var invoice = new Invoice
            {
                AccessKey = model.AccessKey,
                DocumentCode = model.DocumentCode,
                EstablishmentCode = model.Establishment,
                EmissionPointCode = model.EmissionPoint,
                Sequential = model.Sequential,

                IssueDate = DateTime.SpecifyKind(model.IssueDate, DateTimeKind.Utc),

                Ruc = model.Ruc,
                TotalAmount = model.TotalAmount,

                CustomerId = customer.Id,

                CompanyId = company.Id,

                EmissionPointId = emissionPoint.Id,

                JsonData = JsonConvert.SerializeObject(model),

                StatusId = 1,   // "CREATED"
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // ------------------------------
            // 🔹 Crear items de la factura
            // ------------------------------
            foreach (var itemModel in model.Items)
            {
                var item = new InvoiceItem
                {
                    Code = itemModel.Code,
                    AuxCode = itemModel.AuxCode,
                    Description = itemModel.Description,
                    Quantity = itemModel.Quantity,
                    UnitPrice = itemModel.UnitPrice,
                    Discount = itemModel.Discount,
                    TotalWithoutTaxes = itemModel.TotalWithoutTaxes
                };

                // ------------------------------
                // 🔹 Crear impuestos del item
                // ------------------------------
                foreach (var taxModel in itemModel.Taxes)
                {
                    item.Taxes.Add(new InvoiceItemTax
                    {
                        TaxCode = taxModel.TaxCode,
                        PercentageCode = taxModel.PercentageCode,
                        Rate = taxModel.Rate,
                        TaxableBase = taxModel.TaxableBase,
                        Value = taxModel.Value
                    });
                }

                invoice.Items.Add(item);
            }


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
