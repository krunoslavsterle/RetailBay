using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class OrderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    shipping_address_id = table.Column<Guid>(nullable: false),
                    order_status = table.Column<int>(nullable: false),
                    order_total = table.Column<decimal>(nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_identity_user_user_id",
                        column: x => x.user_id,
                        principalTable: "identity_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false),
                    order_id = table.Column<Guid>(nullable: false),
                    prodcut_id = table.Column<Guid>(nullable: false),
                    product_price = table.Column<decimal>(nullable: false),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_product_prodcut_id",
                        column: x => x.prodcut_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "OrderItems",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_prodcut_id",
                table: "OrderItems",
                column: "prodcut_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "Orders",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
