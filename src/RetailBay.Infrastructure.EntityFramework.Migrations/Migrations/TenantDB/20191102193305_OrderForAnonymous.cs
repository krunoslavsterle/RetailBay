using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.TenantDB
{
    public partial class OrderForAnonymous : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_identity_user_user_id",
                table: "order");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "order",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "ix_cart_shipping_address_id",
                table: "cart",
                column: "shipping_address_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cart_address_shipping_address_id",
                table: "cart",
                column: "shipping_address_id",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_order_identity_user_user_id",
                table: "order",
                column: "user_id",
                principalTable: "identity_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_address_shipping_address_id",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "fk_order_identity_user_user_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "ix_cart_shipping_address_id",
                table: "cart");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "order",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_order_identity_user_user_id",
                table: "order",
                column: "user_id",
                principalTable: "identity_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
