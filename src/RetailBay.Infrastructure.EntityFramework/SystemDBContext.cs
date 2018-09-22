using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Core.Entities;

namespace RetailBay.Infrastructure.EntityFramework
{
    public class SystemDBContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("SYSTEM_DB_CONNECTION_STRING"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tenant>(ConfigureTenant);
        }

        private void ConfigureTenant(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("tenant");

            builder.Property(ci => ci.Id)
                .HasColumnName("id")                
                .IsRequired();

            builder.Property(ci => ci.Name)
                .HasColumnName("name")
                .IsRequired(true);

            builder.Property(ci => ci.HostName)
                .HasColumnName("host_name")
                .IsRequired(true);

            builder.Property(ci => ci.ConnectionString)
                .HasColumnName("connection_string")
                .IsRequired(true);

            builder.Property(ci => ci.DateCreated)
                .HasColumnName("date_created")
                .IsRequired(true);

            builder.Property(ci => ci.DateUpdated)
                .HasColumnName("date_updated")
                .IsRequired(true);
        }
    }
}
