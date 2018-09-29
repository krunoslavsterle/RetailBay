using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class ExtendProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_product_category_product_category_id",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "fk_products_product_price_product_price_id",
                table: "product_price");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_category",
                table: "product_category");

            migrationBuilder.AddColumn<string>(
                name: "abrv",
                table: "product_category",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_categories",
                table: "product_category",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_product_categories_product_category_id",
                table: "product",
                column: "product_category_id",
                principalTable: "product_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_products_product_prices_product_price_id",
                table: "product_price",
                column: "id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_product_categories_product_category_id",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "fk_products_product_prices_product_price_id",
                table: "product_price");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_categories",
                table: "product_category");

            migrationBuilder.DropColumn(
                name: "abrv",
                table: "product_category");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_category",
                table: "product_category",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_product_category_product_category_id",
                table: "product",
                column: "product_category_id",
                principalTable: "product_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_products_product_price_product_price_id",
                table: "product_price",
                column: "id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
