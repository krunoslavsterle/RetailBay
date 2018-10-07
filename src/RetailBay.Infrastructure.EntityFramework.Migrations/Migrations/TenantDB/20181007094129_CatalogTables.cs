using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class CatalogTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    contact_name = table.Column<string>(nullable: false),
                    phone = table.Column<string>(nullable: false),
                    street_address = table.Column<string>(nullable: false),
                    postal_code = table.Column<string>(nullable: false),
                    city = table.Column<string>(nullable: false),
                    country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: true),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_identity_user_user_id",
                        column: x => x.user_id,
                        principalTable: "identity_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    abrv = table.Column<string>(maxLength: 20, nullable: false),
                    slug = table.Column<string>(maxLength: 100, nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_address",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    address_id = table.Column<Guid>(nullable: false),
                    address_type = table.Column<int>(nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_address_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_address_identity_user_user_id",
                        column: x => x.user_id,
                        principalTable: "identity_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 500, nullable: false),
                    slug = table.Column<string>(maxLength: 500, nullable: false),
                    description = table.Column<string>(nullable: true),
                    is_published = table.Column<bool>(nullable: false),
                    product_category_id = table.Column<Guid>(nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_product_category_product_category_id",
                        column: x => x.product_category_id,
                        principalTable: "product_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_item",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    cart_id = table.Column<Guid>(nullable: false),
                    product_id = table.Column<Guid>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_item_cart_cart_id",
                        column: x => x.cart_id,
                        principalTable: "cart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cart_item_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_price",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    product_id = table.Column<Guid>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_price", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_price_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cart_user_id",
                table: "cart",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cart_item_cart_id",
                table: "cart_item",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_item_product_id",
                table: "cart_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_product_category_id",
                table: "product",
                column: "product_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_price_product_id",
                table: "product_price",
                column: "product_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_address_address_id",
                table: "user_address",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_address_user_id",
                table: "user_address",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_item");

            migrationBuilder.DropTable(
                name: "product_price");

            migrationBuilder.DropTable(
                name: "user_address");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "product_category");
        }
    }
}
