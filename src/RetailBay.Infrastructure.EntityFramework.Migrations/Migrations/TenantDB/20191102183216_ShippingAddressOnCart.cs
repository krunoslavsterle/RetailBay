using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class ShippingAddressOnCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "shipping_address_id",
                table: "cart",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "address",
                type: "xid",
                nullable: false,
                defaultValue: 0u);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shipping_address_id",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "address");
        }
    }
}
