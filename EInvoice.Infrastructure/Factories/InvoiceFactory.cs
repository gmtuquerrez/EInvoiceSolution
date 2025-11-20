using EInvoice.Infrastructure.Domain.Entities;
using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Models.Dtos;
using Newtonsoft.Json;

namespace EInvoice.Infrastructure.Factories
{
    public static class InvoiceFactory
    {
        public static Invoice CreateEntity(InvoiceModel model, long customerId, long companyId, long emissionPointId)
        {
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

                CustomerId = customerId,
                CompanyId = companyId,
                EmissionPointId = emissionPointId,

                JsonData = JsonConvert.SerializeObject(model),
                StatusId = 1,

                Items = new List<InvoiceItem>(),
                Payments = new List<InvoicePayment>(),
                AdditionalFields = new List<InvoiceAdditionalField>()
            };

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
                    TotalWithoutTaxes = itemModel.TotalWithoutTaxes,
                    Taxes = new List<InvoiceItemTax>()
                };

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

            return invoice;
        }

        public static InvoiceHeaderDto ToHeaderDto(this Invoice invoice)
        {
            return new InvoiceHeaderDto
            {
                Id = invoice.Id,
                AccessKey = invoice.AccessKey,
                DocumentCode = invoice.DocumentCode,
                EstablishmentCode = invoice.EstablishmentCode,
                EmissionPointCode = invoice.EmissionPointCode,
                Sequential = invoice.Sequential,
                IssueDate = invoice.IssueDate,
                Ruc = invoice.Ruc,
                TotalAmount = invoice.TotalAmount,
                CustomerId = invoice.CustomerId,
                CompanyId = invoice.CompanyId,
                EmissionPointId = invoice.EmissionPointId,
                JsonData = invoice.JsonData,
                StatusId = invoice.StatusId,
                XmlGenerated = invoice.XmlGenerated,
                XmlSigned = invoice.XmlSigned,
                XmlAuthorized = invoice.XmlAuthorized,
                SriResponse = invoice.SriResponse
            };
        }
    }
}
