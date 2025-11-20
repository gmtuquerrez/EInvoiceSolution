using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Identity;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services;
using EInvoice.Services.Contracts;
using EInvoiceSolution.SriCheckerConsole.Services;
using EInvoiceSolution.SriCheckerConsole.Workers;
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


// Services
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// Workers
builder.Services.AddSingleton<ICheckerWorker, CheckerWorker>();

var app = builder.Build();

var worker = app.Services.GetRequiredService<ICheckerWorker>();
worker.ExecuteAsync().GetAwaiter().GetResult();
