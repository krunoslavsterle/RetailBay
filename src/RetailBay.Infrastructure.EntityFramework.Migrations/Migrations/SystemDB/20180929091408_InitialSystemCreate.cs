using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.SystemDB
{
    public partial class InitialSystemCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
            migrationBuilder.CreateTable(
                name: "tenant",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    host_name = table.Column<string>(nullable: false),
                    connection_string = table.Column<string>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    date_updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenants", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tenant");
        }
    }
}
