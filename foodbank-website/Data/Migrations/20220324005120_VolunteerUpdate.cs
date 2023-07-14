using Microsoft.EntityFrameworkCore.Migrations;

namespace foodbank_website.Data.Migrations
{
    public partial class VolunteerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventVolunteers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerId = table.Column<string>(nullable: true),
                    EventID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventVolunteers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventVolunteers_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventVolunteers_AspNetUsers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventVolunteers_EventID",
                table: "EventVolunteers",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventVolunteers_VolunteerId",
                table: "EventVolunteers",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventVolunteers");
        }
    }
}
