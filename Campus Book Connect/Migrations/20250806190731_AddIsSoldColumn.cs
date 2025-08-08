using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus_Book_Connect.Migrations
{
    public partial class AddIsSoldColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the column only if it doesn't exist
            migrationBuilder.Sql(@"");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the column only if it exists
            migrationBuilder.Sql(@"");
        }
    }
}
