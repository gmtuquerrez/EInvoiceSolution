using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Identity;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services;
using EInvoice.Services.Contracts;
using EInvoiceSolution.SignerConsole.Services;
using EInvoiceSolution.SignerConsole.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Db context
var connectionString = builder.Configuration
    .GetSection("Configurations:ConnectionStrings:EInvoiceDb")
    .Value;

builder.Services.AddDbContext<EInvoiceDbContext>(options =>
    options.UseNpgsql(connectionString));

// Custom providers
builder.Services.AddScoped<IUserProvider, SystemUserProvider>();

// Repositories
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmissionPointRepository, EmissionPointRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddSingleton<ICompanyCacheService, CompanyCacheService>();


// Services
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// Workers
builder.Services.AddSingleton<ISignerWorker, SignerWorker>();

var app = builder.Build();

var worker = app.Services.GetRequiredService<ISignerWorker>();
worker.ExecuteAsync().GetAwaiter().GetResult();
