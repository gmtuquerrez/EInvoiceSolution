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
        /// Validate an XML string against one or more XSD files.
        /// </summary>
        /// <param name="xml">XML content (UTF-8)</param>
        /// <param name="xsdPaths">Full or relative paths to XSD files required for validation. All referenced XSDs must be included or located next to the main XSD and referenced via schemaLocation.</param>
        /// <returns>XmlValidationResult with list of errors (empty if valid)</returns>
        public XmlValidationResult Validate(string xml, IEnumerable<string> xsdPaths)
        {
            if (xml is null) throw new ArgumentNullException(nameof(xml));
            if (xsdPaths is null) throw new ArgumentNullException(nameof(xsdPaths));

            var result = new XmlValidationResult();

            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                DtdProcessing = DtdProcessing.Prohibit,
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            var schemaSet = new XmlSchemaSet();
            schemaSet.ValidationEventHandler += (s, e) =>
            {
                // schemaSet-level validation events
                result.Errors.Add($"SchemaSet: {e.Severity}: {e.Message}");
            };

            // Load each XSD into the schema set
            foreach (var path in xsdPaths)
            {
                if (!File.Exists(path))
                {
                    result.Errors.Add($"XSD not found: {path}");
                    // continue to try loading other schemas so we report all missing files
                    continue;
                }

                using var stream = File.OpenRead(path);
                using var reader = XmlReader.Create(stream);
                try
                {
                    var schema = XmlSchema.Read(reader, (s, e) =>
                    {
                        result.Errors.Add($"Schema read error ({path}): {e.Severity}: {e.Message}");
                    });

                    if (schema != null)
                        schemaSet.Add(schema);
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Exception reading XSD '{path}': {ex.Message}");
                }
            }

            // If there were missing XSDs, short-circuit (can't validate reliably)
            if (result.Errors.Count > 0 && schemaSet.Count == 0)
                return result;

            settings.Schemas = schemaSet;
            settings.ValidationFlags =
                XmlSchemaValidationFlags.ProcessIdentityConstraints |
                XmlSchemaValidationFlags.ReportValidationWarnings |
                XmlSchemaValidationFlags.ProcessInlineSchema;

            settings.ValidationEventHandler += (sender, e) =>
            {
                // Record validation errors and warnings
                var severity = e.Severity == XmlSeverityType.Error ? "ERROR" : "WARNING";
                // Try to include line info if available
                if (e.Exception?.LineNumber > 0)
                    result.Errors.Add($"{severity} (Line {e.Exception.LineNumber}, Pos {e.Exception.LinePosition}): {e.Message}");
                else
                    result.Errors.Add($"{severity}: {e.Message}");
            };

            // Validate the XML string
            try
            {
                using var stringReader = new StringReader(xml);
                using var xmlReader = XmlReader.Create(stringReader, settings);
                while (xmlReader.Read()) { /* reading triggers validation events */ }
            }
            catch (XmlException xex)
            {
                result.Errors.Add($"XML Exception: {xex.Message} (Line {xex.LineNumber}, Pos {xex.LinePosition})");
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Unexpected exception during validation: {ex.Message}");
            }

            return result;
        }
    }
}
