using InstrumentKaHealth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace InstrumentKaHealth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Oracle specific mappings
            builder.Entity<Instrument>(entity =>
            {
                entity.ToTable("INSTRUMENTS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.InstrumentCode).HasColumnName("INSTRUMENT_CODE");
                entity.Property(e => e.InstrumentName).HasColumnName("INSTRUMENT_NAME");
                entity.Property(e => e.Department).HasColumnName("DEPARTMENT");
                entity.Property(e => e.Location).HasColumnName("LOCATION");
                entity.Property(e => e.Status).HasColumnName("STATUS");
                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
                entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
                entity.Property(e => e.IsApproved).HasColumnName("IS_APPROVED");
            });

            builder.Entity<MaintenanceRecord>(entity =>
            {
                entity.ToTable("MAINTENANCE_RECORDS");
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.MaintenanceRecords)
                    .HasForeignKey(d => d.InstrumentId);
            });
        }
    }
}
