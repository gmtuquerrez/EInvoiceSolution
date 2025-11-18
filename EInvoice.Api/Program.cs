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
    options.UseNpgsql(connectionString));


builder.Services.AddDbContext<EInvoiceDbContext>(options =>
    options.UseNpgsql(connectionString));

// Repositories
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

// Agregar acceso a HttpContext
builder.Services.AddHttpContextAccessor();

// Services
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

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
