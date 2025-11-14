using System.Xml.Serialization;

namespace EInvoice.Infrastructure.Xml
{
    public class XmlSerializerService
    {
        public string Serialize<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, obj);

            return stringWriter.ToString();
        }
    }
}
