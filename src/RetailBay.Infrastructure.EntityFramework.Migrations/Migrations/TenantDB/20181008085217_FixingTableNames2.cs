using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class FixingTableNames2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_items_orders_order_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "fk_order_items_product_prodcut_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_identity_user_user_id",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "pk_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_items",
                table: "order_items");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "order");

            migrationBuilder.RenameTable(
                name: "order_items",
                newName: "order_item");

            migrationBuilder.RenameIndex(
                name: "ix_orders_user_id",
                table: "order",
                newName: "ix_order_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_items_prodcut_id",
                table: "order_item",
                newName: "ix_order_item_prodcut_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_items_order_id",
                table: "order_item",
                newName: "ix_order_item_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order",
                table: "order",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item",
                table: "order_item",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_identity_user_user_id",
                table: "order",
                column: "user_id",
                principalTable: "identity_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_order_order_id",
                table: "order_item",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_product_prodcut_id",
                table: "order_item",
                column: "prodcut_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_identity_user_user_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_order_order_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_product_prodcut_id",
                table: "order_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item",
                table: "order_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order",
                table: "order");

            migrationBuilder.RenameTable(
                name: "order_item",
                newName: "order_items");

            migrationBuilder.RenameTable(
                name: "order",
                newName: "orders");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_prodcut_id",
                table: "order_items",
                newName: "ix_order_items_prodcut_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_order_id",
                table: "order_items",
                newName: "ix_order_items_order_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_user_id",
                table: "orders",
                newName: "ix_orders_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_items",
                table: "order_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_orders",
                table: "orders",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_orders_order_id",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_product_prodcut_id",
                table: "order_items",
                column: "prodcut_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_identity_user_user_id",
                table: "orders",
                column: "user_id",
                principalTable: "identity_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
