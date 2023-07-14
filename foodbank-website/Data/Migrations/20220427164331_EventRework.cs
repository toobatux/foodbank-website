using Microsoft.EntityFrameworkCore.Migrations;

namespace foodbank_website.Data.Migrations
{
    public partial class EventRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentMembers",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "currentMembers",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
