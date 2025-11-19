using EInvoiceSolution.Core.Invoices.Models;
using EInvoiceSolution.Core.Invoices.Mappers;
using EInvoice.Infrastructure.Serialization;

public class InvoiceXmlTests
{
    [Fact]
    public void Should_Map_And_Serialize_Invoice_Correctly()
    {
        // Arrange: Creamos un InvoiceModel válido
        var invoiceModel = new InvoiceModel
        {
            IssueDate = new DateTime(2025, 1, 1),
            Enviroment = "2",
            EmissionType = "1",
            CompanyName = "EMPRESA S.A.",
            TradeMark = "EMPRESA",
            Ruc = "1790012345001",
            AccessKey = "1234567890123456789012345678901234567890123456789",
            DocumentCode = "01",
            Establishment = "001",
            EmissionPoint = "002",
            Sequential = "000000123",
            EstablishmentAddress = "Av. Siempre Viva 123",
            SpecialTaxPayer = "5368",
            RequiredKeepAccounting = "SI",
            CustomerIdentificationType = "05",
            CustomerIdentification = "1452588747",

            TotalAmount = 112.00m,
            TotalWithoutTaxes = 100.00m,
            TotalDiscount = 0,
            Tip = 0,
            Currency = "USD",

            Customer = new CustomerModel
            {
                Identification = "0102030405",
                FullName = "Juan Pérez",
                Address = "Calle Falsa 123"
            },

            TotalWithTaxes = new List<TotalWithTaxesModel>
            {
                new TotalWithTaxesModel
                {
                    TaxCode = "2",
                    PercentageCode = "2",
                    TaxableBase = 100m,
                    Value = 12m
                }
            },

            Items = new List<InvoiceItemModel>
            {
                new InvoiceItemModel
                {
                    Code = "A001",
                    AuxCode = "A001X",
                    Description = "Producto de prueba",
                    Quantity = 1,
                    UnitPrice = 100,
                    Discount = 0,
                    TotalWithoutTaxes = 100,
                    Taxes = new List<TaxesModel>
                    {
                        new TaxesModel
                        {
                            TaxCode = "2",
                            PercentageCode = "2",
                            Rate = 12,
                            TaxableBase = 100,
                            Value = 12
                        }
                    }
                }
            },

            Payments = new List<PaymentModel>
            {
                new PaymentModel
                {
                    Method = "01",
                    Amount = 112,
                    Term = 0,
                    TimeUnit = null
                }
            },

            AdditionalFields = new List<AdditionalFieldModel>
            {
                new AdditionalFieldModel { Name = "Email", Value = "cliente@test.com" },
                new AdditionalFieldModel { Name = "Telefono", Value = "0999999999" }
            }
        };

        // Act: mapeamos
        var factura = InvoiceMapper.MapToSriXsd(invoiceModel);
        Assert.NotNull(factura);

        // Serializamos
        var xml = SriXmlSerializer.SerializeInvoice(factura);
        Assert.False(string.IsNullOrWhiteSpace(xml));

        File.WriteAllText("factura_test.xml", xml);

        // (Opcional) Validación XSD si ya tienes los esquemas
        /*
        var validator = new XmlValidatorService();
        var result = validator.Validate(xml, new[]
        {
            "Schemas/Factura_V1.xsd",
            "Schemas/xmldsig-core-schema.xsd"
        });
        Assert.True(result.IsValid, string.Join("\n", result.Errors));
        */
    }
}
