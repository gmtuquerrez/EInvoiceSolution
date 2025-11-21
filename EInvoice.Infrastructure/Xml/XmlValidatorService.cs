using System.Xml;
using System.Xml.Schema;

namespace EInvoice.Infrastructure.Xml
{
    public class XmlValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; } = new List<string>();
    }

    public class XmlValidatorService
    {
        /// <summary>
        /// Validate an XML string against one or multiple XSD files.
        /// Supports XSDs containing DTD (xmldsig-core-schema.xsd, etc).
        /// </summary>
        public XmlValidationResult Validate(string xml, IEnumerable<string> xsdPaths)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml));
            if (xsdPaths == null) throw new ArgumentNullException(nameof(xsdPaths));

            var result = new XmlValidationResult();

            // Schema validation settings
            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                IgnoreComments = true,
                IgnoreWhitespace = true,
                DtdProcessing = DtdProcessing.Parse, // Allow DTD in XML
                XmlResolver = new XmlUrlResolver()
            };

            // Prepare schema set
            var schemaSet = new XmlSchemaSet
            {
                XmlResolver = new XmlUrlResolver()
            };

            schemaSet.ValidationEventHandler += (s, e) =>
            {
                result.Errors.Add($"XSD Error: {e.Message}");
            };

            // Load XSDs using XmlReader with DTD enabled
            foreach (var xsdPath in xsdPaths)
            {
                if (!File.Exists(xsdPath))
                {
                    result.Errors.Add($"XSD not found: {xsdPath}");
                    continue;
                }

                var xsdSettings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Parse,   // ⭐ important
                    XmlResolver = new XmlUrlResolver()
                };

                using var xsdReader = XmlReader.Create(xsdPath, xsdSettings);

                try
                {
                    schemaSet.Add(null, xsdReader);
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Failed to load XSD '{xsdPath}': {ex.Message}");
                }
            }

            schemaSet.Compile();
            settings.Schemas = schemaSet;

            // Now validate the XML
            using var xmlReader = XmlReader.Create(new StringReader(xml), settings);

            try
            {
                while (xmlReader.Read()) { }
            }
            catch (XmlException ex)
            {
                result.Errors.Add($"XML Error: {ex.Message}");
            }
            catch (XmlSchemaValidationException ex)
            {
                result.Errors.Add($"Schema Validation Error: {ex.Message}");
            }

            return result;
        }
    }
}
