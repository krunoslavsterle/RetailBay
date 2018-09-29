using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class UpdatingEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_product_categories_product_category_id",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "fk_product_product_prices_product_price_id",
                table: "product_price");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_prices",
                table: "product_price");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_categories",
                table: "product_category");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "product_price",
                type: "xid",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AlterColumn<string>(
                name: "slug",
                table: "product_category",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "product_category",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "abrv",
                table: "product_category",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "product_category",
                type: "xid",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "product",
                type: "xid",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_price",
                table: "product_price",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_category",
                table: "product_category",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_product_category_product_category_id",
                table: "product",
                column: "product_category_id",
                principalTable: "product_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_product_price_product_price_id",
                table: "product_price",
                column: "id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_product_category_product_category_id",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "fk_product_product_price_product_price_id",
                table: "product_price");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_price",
                table: "product_price");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_category",
                table: "product_category");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "product_price");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "product_category");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "product");

            migrationBuilder.AlterColumn<string>(
                name: "slug",
                table: "product_category",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "product_category",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "abrv",
                table: "product_category",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_prices",
                table: "product_price",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_categories",
                table: "product_category",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_product_categories_product_category_id",
                table: "product",
                column: "product_category_id",
                principalTable: "product_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_product_prices_product_price_id",
                table: "product_price",
                column: "id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
