using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus_Book_Connect.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUserandUserTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_BuyerId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AppUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "AppUsers",
                newName: "IX_AppUsers_Username");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "AppUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_IdentityUserId",
                table: "AppUsers",
                column: "IdentityUserId",
                unique: true,
                filter: "[IdentityUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AspNetUsers_IdentityUserId",
                table: "AppUsers",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AppUsers_BuyerId",
                table: "Transactions",
                column: "BuyerId",
                principalTable: "AppUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AspNetUsers_IdentityUserId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AppUsers_BuyerId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_IdentityUserId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppUsers");

            migrationBuilder.RenameTable(
                name: "AppUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_Username",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_BuyerId",
                table: "Transactions",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
