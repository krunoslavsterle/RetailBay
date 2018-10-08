using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class OrderShippingAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_item_product_prodcut_id",
                table: "order_item");

            migrationBuilder.RenameColumn(
                name: "prodcut_id",
                table: "order_item",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_prodcut_id",
                table: "order_item",
                newName: "ix_order_item_product_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_shipping_address_id",
                table: "order",
                column: "shipping_address_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_address_shipping_address_id",
                table: "order",
                column: "shipping_address_id",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_product_product_id",
                table: "order_item",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_address_shipping_address_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_product_product_id",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "ix_order_shipping_address_id",
                table: "order");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "order_item",
                newName: "prodcut_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_product_id",
                table: "order_item",
                newName: "ix_order_item_prodcut_id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_product_prodcut_id",
                table: "order_item",
                column: "prodcut_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
