using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class TestTenantMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time_stamp",
                table: "product_price");

            migrationBuilder.DropColumn(
                name: "time_stamp",
                table: "product_category");

            migrationBuilder.DropColumn(
                name: "time_stamp",
                table: "product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<byte[]>(
                name: "time_stamp",
                table: "product",
                rowVersion: true,
                nullable: true);
        }
    }
}
