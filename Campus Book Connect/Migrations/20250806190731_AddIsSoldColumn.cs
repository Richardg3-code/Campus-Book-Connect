using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus_Book_Connect.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSoldColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Books",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Books");
        }
    }
}
