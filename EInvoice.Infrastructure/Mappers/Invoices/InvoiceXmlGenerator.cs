using EInvoice.Infrastructure.Serialization;
using EInvoice.Infrastructure.Xml;
using EInvoiceSolution.Core.Invoices.Models;

namespace EInvoice.Infrastructure.Mappers.Invoices
{
    public static class InvoiceXmlGenerator
    {
        public static string GenerateXml(InvoiceModel model)
        {
            var invoiceXml = InvoiceMapper.MapToSriXsd(model);
            var xml = SriXmlSerializer.SerializeInvoice(invoiceXml);
            var basePath = AppContext.BaseDirectory;

            var validator = new XmlValidatorService();
            var result = validator.Validate(xml, new[]
            {
                Path.Combine(basePath, "Schemas", "Sri", "Invoice", "Xsd", "invoice.xsd"),
                Path.Combine(basePath, "Schemas", "Sri", "Invoice", "Xsd", "xmldsig-core-schema.xsd")
            });

            if (!result.IsValid)
                throw new XmlValidationException(string.Join("\n", result.Errors));

            return xml;
        }
    }

    public class XmlValidationException : Exception
    {
        public XmlValidationException(string message) : base(message) { }
    }

}
