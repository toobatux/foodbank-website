using Microsoft.EntityFrameworkCore.Migrations;

namespace foodbank_website.Data.Migrations
{
    public partial class Updates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Events_EventID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EventID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EventID",
                table: "AspNetUsers",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Events_EventID",
                table: "AspNetUsers",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
