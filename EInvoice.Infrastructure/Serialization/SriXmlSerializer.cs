using System.Text;
using System.Xml;
using System.Xml.Serialization;
using EInvoice.Infrastructure.Sri.Invoice.Generated;

namespace EInvoice.Infrastructure.Serialization
{
    public static class SriXmlSerializer
    {
        public static string SerializeInvoice(Factura factura)
        {
            var xmlSerializer = new XmlSerializer(typeof(Factura));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = new UTF8Encoding(false), // sin BOM
                OmitXmlDeclaration = false
            };

            using var stringWriter = new Utf8StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);

            // Esto evita que agregue namespaces vacíos
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            xmlSerializer.Serialize(xmlWriter, factura, namespaces);

            return stringWriter.ToString();
        }
    }

    // Writer UTF-8 sin BOM
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => new UTF8Encoding(false);
    }
}
