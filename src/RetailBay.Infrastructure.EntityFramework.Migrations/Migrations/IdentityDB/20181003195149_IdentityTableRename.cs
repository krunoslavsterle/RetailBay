using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailBay.Infrastructure.EntityFramework.Migrations.Migrations.IdentityDB
{
    public partial class IdentityTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                table: "asp_net_role_claims");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_claims_asp_net_users_user_id",
                table: "asp_net_user_claims");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_logins_asp_net_users_user_id",
                table: "asp_net_user_logins");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_roles_asp_net_users_user_id",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                table: "asp_net_user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_users",
                table: "asp_net_users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_tokens",
                table: "asp_net_user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_roles",
                table: "asp_net_user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_logins",
                table: "asp_net_user_logins");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_claims",
                table: "asp_net_user_claims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_roles",
                table: "asp_net_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_role_claims",
                table: "asp_net_role_claims");

            migrationBuilder.RenameTable(
                name: "asp_net_users",
                newName: "identity_user");

            migrationBuilder.RenameTable(
                name: "asp_net_user_tokens",
                newName: "identity_user_token");

            migrationBuilder.RenameTable(
                name: "asp_net_user_roles",
                newName: "identity_user_role");

            migrationBuilder.RenameTable(
                name: "asp_net_user_logins",
                newName: "identity_user_login");

            migrationBuilder.RenameTable(
                name: "asp_net_user_claims",
                newName: "identity_user_claim");

            migrationBuilder.RenameTable(
                name: "asp_net_roles",
                newName: "identity_role");

            migrationBuilder.RenameTable(
                name: "asp_net_role_claims",
                newName: "identity_role_claim");

            migrationBuilder.RenameIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "identity_user_role",
                newName: "ix_identity_user_role_role_id");

            migrationBuilder.RenameIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "identity_user_login",
                newName: "ix_identity_user_login_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "identity_user_claim",
                newName: "ix_identity_user_claim_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "identity_role_claim",
                newName: "ix_identity_role_claim_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_user",
                table: "identity_user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_user_token",
                table: "identity_user_token",
                columns: new[] { "user_id", "login_provider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_user_role",
                table: "identity_user_role",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_user_login",
                table: "identity_user_login",
                columns: new[] { "login_provider", "provider_key" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_user_claim",
                table: "identity_user_claim",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_role",
                table: "identity_role",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_identity_role_claim",
                table: "identity_role_claim",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_identity_role_claim_identity_role_role_id",
                table: "identity_role_claim",
                column: "role_id",
                principalTable: "identity_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_identity_user_claim_identity_user_user_id",
                table: "identity_user_claim",
                column: "user_id",
                principalTable: "identity_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_identity_user_login_identity_user_user_id",
                table: "identity_user_login",
                column: "user_id",
                principalTable: "identity_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_identity_user_role_identity_role_role_id",
                table: "identity_user_role",
                column: "role_id",
                principalTable: "identity_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_identity_user_role_identity_user_user_id",
                table: "identity_user_role",
                column: "user_id",
                principalTable: "identity_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_identity_user_token_identity_user_user_id",
                table: "identity_user_token",
                column: "user_id",
                principalTable: "identity_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_identity_role_claim_identity_role_role_id",
                table: "identity_role_claim");

            migrationBuilder.DropForeignKey(
                name: "fk_identity_user_claim_identity_user_user_id",
                table: "identity_user_claim");

            migrationBuilder.DropForeignKey(
                name: "fk_identity_user_login_identity_user_user_id",
                table: "identity_user_login");

            migrationBuilder.DropForeignKey(
                name: "fk_identity_user_role_identity_role_role_id",
                table: "identity_user_role");

            migrationBuilder.DropForeignKey(
                name: "fk_identity_user_role_identity_user_user_id",
                table: "identity_user_role");

            migrationBuilder.DropForeignKey(
                name: "fk_identity_user_token_identity_user_user_id",
                table: "identity_user_token");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_user_token",
                table: "identity_user_token");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_user_role",
                table: "identity_user_role");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_user_login",
                table: "identity_user_login");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_user_claim",
                table: "identity_user_claim");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_user",
                table: "identity_user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_role_claim",
                table: "identity_role_claim");

            migrationBuilder.DropPrimaryKey(
                name: "pk_identity_role",
                table: "identity_role");

            migrationBuilder.RenameTable(
                name: "identity_user_token",
                newName: "asp_net_user_tokens");

            migrationBuilder.RenameTable(
                name: "identity_user_role",
                newName: "asp_net_user_roles");

            migrationBuilder.RenameTable(
                name: "identity_user_login",
                newName: "asp_net_user_logins");

            migrationBuilder.RenameTable(
                name: "identity_user_claim",
                newName: "asp_net_user_claims");

            migrationBuilder.RenameTable(
                name: "identity_user",
                newName: "asp_net_users");

            migrationBuilder.RenameTable(
                name: "identity_role_claim",
                newName: "asp_net_role_claims");

            migrationBuilder.RenameTable(
                name: "identity_role",
                newName: "asp_net_roles");

            migrationBuilder.RenameIndex(
                name: "ix_identity_user_role_role_id",
                table: "asp_net_user_roles",
                newName: "ix_asp_net_user_roles_role_id");

            migrationBuilder.RenameIndex(
                name: "ix_identity_user_login_user_id",
                table: "asp_net_user_logins",
                newName: "ix_asp_net_user_logins_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_identity_user_claim_user_id",
                table: "asp_net_user_claims",
                newName: "ix_asp_net_user_claims_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_identity_role_claim_role_id",
                table: "asp_net_role_claims",
                newName: "ix_asp_net_role_claims_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_tokens",
                table: "asp_net_user_tokens",
                columns: new[] { "user_id", "login_provider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_roles",
                table: "asp_net_user_roles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_logins",
                table: "asp_net_user_logins",
                columns: new[] { "login_provider", "provider_key" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_claims",
                table: "asp_net_user_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_users",
                table: "asp_net_users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_role_claims",
                table: "asp_net_role_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_roles",
                table: "asp_net_roles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                table: "asp_net_role_claims",
                column: "role_id",
                principalTable: "asp_net_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_claims_asp_net_users_user_id",
                table: "asp_net_user_claims",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_logins_asp_net_users_user_id",
                table: "asp_net_user_logins",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                table: "asp_net_user_roles",
                column: "role_id",
                principalTable: "asp_net_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_roles_asp_net_users_user_id",
                table: "asp_net_user_roles",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                table: "asp_net_user_tokens",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
