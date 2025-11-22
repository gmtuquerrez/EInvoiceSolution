using Microsoft.Extensions.Configuration;

namespace EInvoice.Services.Configuration
{
    public class ConfigurationsManager
    {
        public string ConnectionString { get; set; } = string.Empty;
        public SriSettings Sri { get; set; } = new();
        public bool IsProduction { get; set; }
    }

    public class SriSettings
    {
        public string ApiUrl { get; set; } = string.Empty;
    }
}
