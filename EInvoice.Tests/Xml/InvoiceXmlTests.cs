using System;
using System.IO;
using Xunit;
using EInvoice.Infrastructure.Xml;
using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Mappers;
using EInvoice.Infrastructure.Serialization;
using System.Collections.Generic;

public class InvoiceXmlTests
{
    [Fact]
    public void InvoiceXml_Should_Serialize_And_Validate_Successfully()
    {
        var invoiceModel = new InvoiceModel
        {
            InvoiceNumber = "001-001-000000123",
            IssueDate = DateTime.Parse("2025-01-01"),
            BuyerName = "Cliente de Prueba",
            BuyerId = "0102030405",
            Subtotal = 100,
            Total = 112,

            Payments = new List<PaymentModel>
            {
                new PaymentModel
                {
                    Method = "01",
                    Amount = 112,
                    Term = 0,
                    TimeUnit = "dias"
                }
            },

            Items = new List<InvoiceItemModel>
            {
                new InvoiceItemModel
                {
                    Description = "Producto A",
                    Quantity = 1,
                    UnitPrice = 100,
                    Total = 100
                }
            }
        };

        var facturaXsd = InvoiceMapper.MapToSriXsd(invoiceModel);
        Assert.NotNull(facturaXsd);

        var xml = SriXmlSerializer.SerializeInvoice(facturaXsd);
        Assert.False(string.IsNullOrWhiteSpace(xml));

        File.WriteAllText("factura_test.xml", xml);

        var xsdPaths = new[]
        {
            "Schemas/Sri/Invoice/Xsd/invoice.xsd",
            "Schemas/Sri/Invoice/Xsd/xmldsig-core-schema.xsd"
        };

        var validator = new XmlValidatorService();
        var result = validator.Validate(xml, xsdPaths);

        Assert.True(result.IsValid, "XML inválido:\n" + string.Join("\n", result.Errors));
    }
}
