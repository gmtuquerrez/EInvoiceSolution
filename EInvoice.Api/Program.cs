using EInvoice.Api.Common.Security;
using EInvoice.Infrastructure.Db;
using EInvoice.Infrastructure.Identity;
using EInvoice.Infrastructure.Repositories;
using EInvoice.Services;
using EInvoice.Services.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom providers
builder.Services.AddScoped<IUserProvider, HttpContextUserProvider>();

// DbContext

var connectionString = builder.Configuration
    .GetSection("Configurations:ConnectionStrings:EInvoiceDb")
    .Value;

builder.Services.AddDbContext<EInvoiceDbContext>(options =>
    options.UseNpgsql(connectionString).EnableSensitiveDataLogging()     // ← DELETE
           .EnableDetailedErrors());


builder.Services.AddDbContext<EInvoiceDbContext>(options =>
    options.UseNpgsql(connectionString));

// Agregar acceso a HttpContext
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

// Print current environment
var env = app.Services.GetRequiredService<IWebHostEnvironment>();
Console.WriteLine($"Current Environment: {env.EnvironmentName}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
