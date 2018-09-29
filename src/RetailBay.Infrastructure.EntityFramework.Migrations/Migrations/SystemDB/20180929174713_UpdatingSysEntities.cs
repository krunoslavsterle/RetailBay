using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.SystemDB
{
    public partial class UpdatingSysEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_tenants",
                table: "tenant");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "tenant",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "host_name",
                table: "tenant",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<byte[]>(
                name: "time_stamp",
                table: "tenant",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_tenant",
                table: "tenant",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_tenant",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "time_stamp",
                table: "tenant");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "tenant",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "host_name",
                table: "tenant",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "pk_tenants",
                table: "tenant",
                column: "id");
        }
    }
}
