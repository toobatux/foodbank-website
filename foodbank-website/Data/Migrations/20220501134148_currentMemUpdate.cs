using Microsoft.EntityFrameworkCore.Migrations;

namespace foodbank_website.Data.Migrations
{
    public partial class currentMemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "currentMembers",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentMembers",
                table: "Events");
        }
    }
}
