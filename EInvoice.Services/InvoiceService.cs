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

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceCreatedResponse> CreateInvoiceAsync(InvoiceModel model, string createdBy)
        {
            // 1. Mapear InvoiceModel → Entity Invoice
            var entity = new Invoice
            {
                AccessKey = model.AccessKey,
                DocumentCode = model.DocumentCode,
                Establishment = model.Establishment,
                EmissionPoint = model.EmissionPoint,
                Sequential = model.Sequential,
                IssueDate = model.IssueDate,
                Ruc = model.Ruc,
                TotalAmount = model.TotalAmount,
                CustomerId = model.Customer.Id,  // Asumes Customer ya existe

                JsonData = JsonConvert.SerializeObject(model),

                StatusId = 1,   // "CREATED"
                CreatedBy = createdBy,
                UpdatedBy = createdBy,
            };

            // 2. Guardamos en base
            var created = await _invoiceRepository.CreateAsync(entity);

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
