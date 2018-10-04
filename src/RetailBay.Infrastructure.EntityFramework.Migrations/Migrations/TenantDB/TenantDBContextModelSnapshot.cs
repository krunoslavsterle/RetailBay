﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RetailBay.Infrastructure.EntityFramework;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    [DbContext(typeof(TenantDBContext))]
    partial class TenantDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ClaimType")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("RoleId")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_identity_role_claim");

                    b.HasIndex("RoleId")
                        .HasName("ix_identity_role_claim_role_id");

                    b.ToTable("identity_role_claim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ClaimType")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_identity_user_claim");

                    b.HasIndex("UserId")
                        .HasName("ix_identity_user_claim_user_id");

                    b.ToTable("identity_user_claim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnName("provider_display_name");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_identity_user_login");

                    b.HasIndex("UserId")
                        .HasName("ix_identity_user_login_user_id");

                    b.ToTable("identity_user_login");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_identity_user_role");

                    b.HasIndex("RoleId")
                        .HasName("ix_identity_user_role_role_id");

                    b.ToTable("identity_user_role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_identity_user_token");

                    b.ToTable("identity_user_token");
                });

            modelBuilder.Entity("RetailBay.Core.Entities.Identity.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnName("normalized_name")
                        .HasMaxLength(256);

                    b.HasKey("Id")
                        .HasName("pk_identity_role");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("role_name_index");

                    b.ToTable("identity_role");
                });

            modelBuilder.Entity("RetailBay.Core.Entities.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnName("normalized_email")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnName("normalized_user_name")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasColumnName("user_name")
                        .HasMaxLength(256);

                    b.HasKey("Id")
                        .HasName("pk_identity_user");

                    b.HasIndex("NormalizedEmail")
                        .HasName("email_index");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("user_name_index");

                    b.ToTable("identity_user");
                });

            modelBuilder.Entity("RetailBay.Core.Entities.TenantDB.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnName("date_updated");

                    b.Property<Guid?>("UserId")
                        .HasColumnName("user_id");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid");

                    b.HasKey("Id")
                        .HasName("pk_cart");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasName("ix_cart_user_id");

                    b.ToTable("cart");
                });

            modelBuilder.Entity("RetailBay.Core.Entities.TenantDB.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<Guid>("CartId")
                        .HasColumnName("cart_id");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnName("date_updated");

                    b.Property<Guid>("ProductId")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid");

                    b.HasKey("Id")
                        .HasName("pk_cart_item");

                    b.HasIndex("CartId")
                        .HasName("ix_cart_item_cart_id");

                    b.HasIndex("ProductId")
                        .HasName("ix_cart_item_product_id");

                    b.ToTable("cart_item");
                });

            modelBuilder.Entity("RetailBay.Core.Entities.TenantDB.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnName("date_updated");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<bool>("IsPublished")
                        .HasColumnName("is_published");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(500);

                    b.Property<Guid>("ProductCategoryId")
                        .HasColumnName("product_category_id");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnName("slug")
                        .HasMaxLength(500);

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid");

                    b.HasKey("Id")
                        .HasName("pk_product");

                    b.HasIndex("ProductCategoryId")
                        .HasName("ix_product_product_category_id");

                    b.ToTable("product");
                });

            modelBuilder.Entity("RetailBay.Core.Entities.TenantDB.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Abrv")
                        .IsRequired()
                        .HasColumnName("abrv")
                        .HasMaxLength(20);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnName("date_updated");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(100);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnName("slug")
                        .HasMaxLength(100);

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid");

                    b.HasKey("Id")
                        .HasName("pk_product_category");

                    b.ToTable("product_category");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("RetailBay.Core.Entities.Identity.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_identity_role_claim_identity_role_role_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("RetailBay.Core.Entities.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_identity_user_claim_identity_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("RetailBay.Core.Entities.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_identity_user_login_identity_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("RetailBay.Core.Entities.Identity.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_identity_user_role_identity_role_role_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RetailBay.Core.Entities.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_identity_user_role_identity_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("RetailBay.Core.Entities.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_identity_user_token_identity_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RetailBay.Core.Entities.TenantDB.Cart", b =>
                {
                    b.HasOne("RetailBay.Core.Entities.Identity.ApplicationUser", "User")
                        .WithOne("Cart")
                        .HasForeignKey("RetailBay.Core.Entities.TenantDB.Cart", "UserId")
                        .HasConstraintName("fk_cart_identity_user_user_id");
                });

            modelBuilder.Entity("RetailBay.Core.Entities.TenantDB.CartItem", b =>
                {
                    b.HasOne("RetailBay.Core.Entities.TenantDB.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .HasConstraintName("fk_cart_item_cart_cart_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RetailBay.Core.Entities.TenantDB.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fk_cart_item_product_product_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RetailBay.Core.Entities.TenantDB.Product", b =>
                {
                    b.HasOne("RetailBay.Core.Entities.TenantDB.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .HasConstraintName("fk_product_product_category_product_category_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("RetailBay.Core.Entities.TenantDB.ProductPrice", "ProductPrice", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnName("id");

                            b1.Property<DateTime>("DateCreated")
                                .HasColumnName("date_created");

                            b1.Property<DateTime>("DateUpdated")
                                .HasColumnName("date_updated");

                            b1.Property<decimal>("Price")
                                .HasColumnName("price");

                            b1.Property<uint>("xmin")
                                .IsConcurrencyToken()
                                .ValueGeneratedOnAddOrUpdate()
                                .HasColumnType("xid");

                            b1.ToTable("product_price");

                            b1.HasOne("RetailBay.Core.Entities.TenantDB.Product", "Product")
                                .WithOne("ProductPrice")
                                .HasForeignKey("RetailBay.Core.Entities.TenantDB.ProductPrice", "Id")
                                .HasConstraintName("fk_product_product_price_product_price_id")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
