using EInvoice.Infrastructure.Domain.Common;
using EInvoice.Infrastructure.Domain.Entities;
using EInvoice.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EInvoice.Infrastructure.Db
{
    public class EInvoiceDbContext : DbContext
    {
        private readonly IUserProvider _userProvider;

        public EInvoiceDbContext(DbContextOptions<EInvoiceDbContext> options, IUserProvider userProvider)
            : base(options)
        {
            _userProvider = userProvider;
        }

        // DbSets
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Establishment> Establishments => Set<Establishment>();
        public DbSet<EmissionPoint> EmissionPoints => Set<EmissionPoint>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<InvoiceStatus> InvoiceStatuses => Set<InvoiceStatus>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
        public DbSet<InvoiceItemTax> InvoiceItemTaxes => Set<InvoiceItemTax>();
        public DbSet<InvoicePayment> InvoicePayments => Set<InvoicePayment>();
        public DbSet<InvoiceAdditionalField> InvoiceAdditionalFields => Set<InvoiceAdditionalField>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("einvoice");

            // Company
            modelBuilder.Entity<Company>(b =>
            {
                b.ToTable("Company");
                b.HasKey(x => x.Id);
                b.Property(x => x.BusinessName).HasColumnName("BusinessName").HasMaxLength(200).IsRequired();
                b.Property(x => x.Ruc).HasColumnName("Ruc").HasMaxLength(13).IsRequired();
                b.Property(x => x.SignatureBase64).HasColumnName("SignatureBase64").IsRequired();
                b.Property(x => x.SignaturePassword).HasColumnName("SignaturePassword").HasMaxLength(200).IsRequired();
                b.Property(x => x.LogoBase64).HasColumnName("LogoBase64");
            });

            // Establishment
            modelBuilder.Entity<Establishment>(b =>
            {
                b.ToTable("Establishment");
                b.HasKey(x => x.Id);
                b.Property(x => x.Code).HasColumnName("Code").HasMaxLength(3).IsRequired();
                b.Property(x => x.Address).HasColumnName("Address").HasMaxLength(300).IsRequired();
                b.HasOne(x => x.Company).WithMany(c => c.Establishments).HasForeignKey(x => x.CompanyId);
                b.HasIndex(x => new { x.CompanyId, x.Code }).IsUnique().HasDatabaseName("UQ_Establishment_Company_Code");
            });

            // EmissionPoint
            modelBuilder.Entity<EmissionPoint>(b =>
            {
                b.ToTable("EmissionPoint");
                b.HasKey(x => x.Id);
                b.Property(x => x.Code).HasColumnName("Code").HasMaxLength(3).IsRequired();
                b.HasOne(x => x.Establishment).WithMany(e => e.EmissionPoints).HasForeignKey(x => x.EstablishmentId);
                b.HasIndex(x => new { x.EstablishmentId, x.Code }).IsUnique().HasDatabaseName("UQ_EmissionPoint_Estab_Code");
            });

            // Customers
            modelBuilder.Entity<Customer>(b =>
            {
                b.ToTable("Customers");
                b.HasKey(x => x.Id);
                b.Property(x => x.Identification).HasColumnName("Identification").IsRequired().HasMaxLength(20);
                b.Property(x => x.FullName).HasColumnName("FullName").IsRequired().HasMaxLength(200);
                b.HasIndex(x => x.Identification).IsUnique().HasDatabaseName("UQ_Customers_Identification");
            });

            // InvoiceStatus
            modelBuilder.Entity<InvoiceStatus>(b =>
            {
                b.ToTable("InvoiceStatus");
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(40);
            });

            modelBuilder.Entity<Invoice>(b =>
            {
                b.ToTable("Invoices");
                b.HasKey(x => x.Id);

                b.Property(x => x.AccessKey).HasMaxLength(49).IsRequired();
                b.Property(x => x.DocumentCode).HasMaxLength(3).IsRequired();
                b.Property(x => x.EstablishmentCode).HasColumnName("Establishment").HasMaxLength(3).IsRequired();
                b.Property(x => x.EmissionPointCode).HasColumnName("EmissionPoint").HasMaxLength(3).IsRequired();
                b.Property(x => x.Sequential).HasMaxLength(20).IsRequired();

                b.Property(x => x.IssueDate).IsRequired();
                b.Property(x => x.AuthorizationDate);

                b.Property(x => x.Ruc).HasMaxLength(13).IsRequired();
                b.Property(x => x.TotalAmount).HasColumnType("numeric(18,2)").IsRequired();

                b.Property(x => x.JsonData).HasColumnType("jsonb").IsRequired();

                b.HasOne(x => x.Customer)
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Status)
                    .WithMany()
                    .HasForeignKey(x => x.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.EmissionPoint)
                    .WithMany()
                    .HasForeignKey(x => x.EmissionPointId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Company)
                    .WithMany(c => c.Invoices)       // ← clave
                    .HasForeignKey(x => x.CompanyId) // ← clave
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // InvoiceItem
            modelBuilder.Entity<InvoiceItem>(b =>
            {
                b.ToTable("InvoiceItems");
                b.HasKey(x => x.Id);
                b.Property(x => x.Code).HasColumnName("Code").HasMaxLength(50).IsRequired();
                b.Property(x => x.AuxCode).HasColumnName("AuxCode").HasMaxLength(50);
                b.Property(x => x.Description).HasColumnName("Description").HasMaxLength(300).IsRequired();
                b.Property(x => x.Quantity).HasColumnName("Quantity").HasColumnType("numeric(18,6)").IsRequired();
                b.Property(x => x.UnitPrice).HasColumnName("UnitPrice").HasColumnType("numeric(18,6)").IsRequired();
                b.Property(x => x.Discount).HasColumnName("Discount").HasColumnType("numeric(18,6)").IsRequired();
                b.Property(x => x.TotalWithoutTaxes).HasColumnName("TotalWithoutTaxes").HasColumnType("numeric(18,6)").IsRequired();

                b.HasOne(x => x.Invoice).WithMany(i => i.Items).HasForeignKey(x => x.InvoiceId);
            });

            // InvoiceItemTax
            modelBuilder.Entity<InvoiceItemTax>(b =>
            {
                b.ToTable("InvoiceItemTaxes");
                b.HasKey(x => x.Id);
                b.Property(x => x.TaxCode).HasColumnName("TaxCode").HasMaxLength(3).IsRequired();
                b.Property(x => x.PercentageCode).HasColumnName("PercentageCode").HasMaxLength(3).IsRequired();
                b.Property(x => x.Rate).HasColumnName("Rate").HasColumnType("numeric(18,6)");
                b.Property(x => x.TaxableBase).HasColumnName("TaxableBase").HasColumnType("numeric(18,6)").IsRequired();
                b.Property(x => x.Value).HasColumnName("Value").HasColumnType("numeric(18,6)").IsRequired();

                b.HasOne(x => x.InvoiceItem).WithMany(i => i.Taxes).HasForeignKey(x => x.InvoiceItemId);
            });

            // InvoicePayment
            modelBuilder.Entity<InvoicePayment>(b =>
            {
                b.ToTable("InvoicePayments");
                b.HasKey(x => x.Id);
                b.Property(x => x.Method).HasColumnName("Method").HasMaxLength(10).IsRequired();
                b.Property(x => x.Amount).HasColumnName("Amount").HasColumnType("numeric(18,2)").IsRequired();
                b.Property(x => x.Term).HasColumnName("Term");
                b.Property(x => x.TimeUnit).HasColumnName("TimeUnit").HasMaxLength(10);

                b.HasOne(x => x.Invoice).WithMany(i => i.Payments).HasForeignKey(x => x.InvoiceId);
            });

            // InvoiceAdditionalField
            modelBuilder.Entity<InvoiceAdditionalField>(b =>
            {
                b.ToTable("InvoiceAdditionalFields");
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
                b.Property(x => x.Value).HasColumnName("Value").HasMaxLength(500).IsRequired();

                b.HasOne(x => x.Invoice).WithMany(i => i.AdditionalFields).HasForeignKey(x => x.InvoiceId);
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ApplyAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAudit()
        {
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            var user = _userProvider?.GetCurrentUser() ?? "SYSTEM";

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                // --- CORRIGE DateTime.Unspecified -> UTC ---
                foreach (var prop in entry.Properties.Where(p =>
                    p.Metadata.ClrType == typeof(DateTime) ||
                    p.Metadata.ClrType == typeof(DateTime?)))
                {
                    if (prop.CurrentValue is DateTime dt)
                    {
                        if (dt.Kind == DateTimeKind.Unspecified)
                        {
                            // Normalizar a UTC
                            prop.CurrentValue = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                        }
                    }
                }

                // --- AUDITORÍA ---
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy ??= user;
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = user;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = user;
                }
            }
        }


    }
}
