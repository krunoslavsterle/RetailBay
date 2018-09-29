using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class ProductDataAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_product_categories_product_category_id",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "fk_products_product_prices_product_price_id",
                table: "product_price");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_price",
                table: "product_price");

            migrationBuilder.DropPrimaryKey(
                name: "pk_products",
                table: "product");

            migrationBuilder.RenameIndex(
                name: "ix_products_product_category_id",
                table: "product",
                newName: "ix_product_product_category_id");

            migrationBuilder.AddColumn<byte[]>(
                name: "time_stamp",
                table: "product_price",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "time_stamp",
                table: "product_category",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "slug",
                table: "product",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "product",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "product",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<byte[]>(
                name: "time_stamp",
                table: "product",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_prices",
                table: "product_price",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product",
                table: "product",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "pk_product",
                table: "product");

            migrationBuilder.DropColumn(
                name: "time_stamp",
                table: "product_price");

            migrationBuilder.DropColumn(
                name: "time_stamp",
                table: "product_category");

            migrationBuilder.DropColumn(
                name: "time_stamp",
                table: "product");

            migrationBuilder.RenameIndex(
                name: "ix_product_product_category_id",
                table: "product",
                newName: "ix_products_product_category_id");

            migrationBuilder.AlterColumn<string>(
                name: "slug",
                table: "product",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "product",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "product",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_price",
                table: "product_price",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_products",
                table: "product",
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
    }
}
