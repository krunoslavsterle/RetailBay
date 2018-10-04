﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework
{
    public class TenantDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly string _connectionString;
        public TenantDBContext(Tenant tenant)
        {
            _connectionString = tenant.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString, options => options.MigrationsAssembly("RetailBay.Infrastructure.EntityFramework.Migrations"));

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories {get; set;}
        public DbSet<ProductPrice> ProductPrices { get; set; }
       // public DbSet<Cart> Carts { get; set; }
        //public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("identity_user");
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("identity_role");
            });

            builder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.ToTable("identity_user_role");
            });

            builder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.ToTable("identity_role_claim");
            });

            builder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.ToTable("identity_user_claim");
            });

            builder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.ToTable("identity_user_login");
            });

            builder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.ToTable("identity_user_token");
            });

            builder.UseSnakeCaseNamingConvention(true);

            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<ProductPrice>(ConfigureProductPrice);
            builder.Entity<ProductCategory>(ConfigureProductCategory);
           // builder.Entity<Cart>(ConfigureCart);
           // builder.Entity<CartItem>(ConfigureCartItem);
        }

        private void ConfigureProductCategory(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }

        private void ConfigureProductPrice(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();
            builder.OwnsOne(p => p.ProductPrice).HasForeignKey(p => p.Id);

            builder.HasOne(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }

        private void ConfigureCart(EntityTypeBuilder<Cart> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();

            builder.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Cart>(p => p.UserId);
        }

        private void ConfigureCartItem(EntityTypeBuilder<CartItem> builder)
        {
            builder.ForNpgsqlUseXminAsConcurrencyToken();

            builder.HasOne(p => p.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(p => p.CartId);

            //builder.HasOne(p => p.Product)
            //    .WithMany(p => p.CartItems)
            //    .HasForeignKey(p => p.ProductId);
        }
    }
}
