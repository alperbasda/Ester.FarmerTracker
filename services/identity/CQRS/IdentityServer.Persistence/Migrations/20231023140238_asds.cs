using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class asds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceClientRelations_ApiResources_ClientId",
                table: "ApiResourceClientRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceClientRelations_Clients_ApiResourceId",
                table: "ApiResourceClientRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserRelations_Clients_UserId",
                table: "ClientUserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserRelations_Users_ClientId",
                table: "ClientUserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUserRelations_Roles_UserId",
                table: "RoleUserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUserRelations_Users_RoleId",
                table: "RoleUserRelations");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceClientRelations_ApiResources_ApiResourceId",
                table: "ApiResourceClientRelations",
                column: "ApiResourceId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceClientRelations_Clients_ClientId",
                table: "ApiResourceClientRelations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserRelations_Clients_ClientId",
                table: "ClientUserRelations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserRelations_Users_UserId",
                table: "ClientUserRelations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUserRelations_Roles_RoleId",
                table: "RoleUserRelations",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUserRelations_Users_UserId",
                table: "RoleUserRelations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceClientRelations_ApiResources_ApiResourceId",
                table: "ApiResourceClientRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ApiResourceClientRelations_Clients_ClientId",
                table: "ApiResourceClientRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserRelations_Clients_ClientId",
                table: "ClientUserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserRelations_Users_UserId",
                table: "ClientUserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUserRelations_Roles_RoleId",
                table: "RoleUserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUserRelations_Users_UserId",
                table: "RoleUserRelations");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceClientRelations_ApiResources_ClientId",
                table: "ApiResourceClientRelations",
                column: "ClientId",
                principalTable: "ApiResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApiResourceClientRelations_Clients_ApiResourceId",
                table: "ApiResourceClientRelations",
                column: "ApiResourceId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserRelations_Clients_UserId",
                table: "ClientUserRelations",
                column: "UserId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserRelations_Users_ClientId",
                table: "ClientUserRelations",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUserRelations_Roles_UserId",
                table: "RoleUserRelations",
                column: "UserId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUserRelations_Users_RoleId",
                table: "RoleUserRelations",
                column: "RoleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
