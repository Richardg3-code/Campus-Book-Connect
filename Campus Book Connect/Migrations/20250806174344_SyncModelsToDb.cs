using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus_Book_Connect.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Books",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Books");
        }
    }
}
