using Microsoft.EntityFrameworkCore.Migrations;

namespace BackAgain.Migrations
{
    public partial class settingsUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LabelColor",
                table: "_UserSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainTextColor",
                table: "_UserSettings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelColor",
                table: "_UserSettings");

            migrationBuilder.DropColumn(
                name: "MainTextColor",
                table: "_UserSettings");
        }
    }
}
