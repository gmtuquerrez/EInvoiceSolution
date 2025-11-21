namespace EInvoice.Services.Configuration
{
    public class ConfigurationsManager
    {
        public ConnectionStrings ConnectionString { get; set; } = new();
        public SriSettings Sri { get; set; } = new();
        public bool IsProduction { get; set; }
    }
    public class ConnectionStrings
    {
        public string EInvoiceDb { get; set; } = string.Empty;
    }

    public class SriSettings
    {
        public string ApiUrl { get; set; } = string.Empty;
    }
}
