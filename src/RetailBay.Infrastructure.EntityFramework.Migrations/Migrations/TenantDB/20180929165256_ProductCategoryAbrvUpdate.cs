using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class ProductCategoryAbrvUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "abrv",
                table: "product_category",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "abrv",
                table: "product_category",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
