using EInvoice.Api.Common.Security;
using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Identity;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services;
using EInvoice.Services.Configuration;
using EInvoice.Services.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom providers
builder.Services.AddScoped<IUserProvider, HttpContextUserProvider>();

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
{
    options.UseNpgsql(conf.ConnectionString);

    if (!conf.IsProduction)
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// HttpContext
builder.Services.AddHttpContextAccessor();

// Repositories
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmissionPointRepository, EmissionPointRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

// Services
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Logging.AddConsole();

var app = builder.Build();

// Swagger solo en DEV
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
