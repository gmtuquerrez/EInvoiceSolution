using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Identity;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services;
using EInvoice.Services.Configuration;
using EInvoice.Services.Contracts;
using EInvoiceSolution.SignerConsole.Services;
using EInvoiceSolution.SignerConsole.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Load configurations
var confSection = builder.Configuration.GetSection("Configurations");

if (!confSection.Exists())
{
    throw new InvalidOperationException("Section 'Configurations' not found in configuration.");
}

var conf = confSection.Get<ConfigurationsManager>()!;

// Detect environment
var env =
    Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
    "Development";

Console.WriteLine($"Current Environment: {env}");

conf.IsProduction = env.Equals("Production", StringComparison.OrdinalIgnoreCase);

// DbContext
builder.Services.AddDbContext<EInvoiceDbContext>(options =>
    options.UseNpgsql(conf.ConnectionString));


// Custom providers
builder.Services.AddScoped<IUserProvider, SystemUserProvider>();

// Repositories
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmissionPointRepository, EmissionPointRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyCacheService, CompanyCacheService>();


// Services
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// Workers
builder.Services.AddScoped<ISignerWorker, SignerWorker>();

var app = builder.Build();

// Create a scope explicitly
using (var scope = app.Services.CreateScope())
{
    var worker = scope.ServiceProvider.GetRequiredService<ISignerWorker>();
    worker.ExecuteAsync().GetAwaiter().GetResult();
}