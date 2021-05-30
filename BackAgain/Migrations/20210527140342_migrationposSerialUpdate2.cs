using Microsoft.EntityFrameworkCore.Migrations;

namespace BackAgain.Migrations
{
    public partial class migrationposSerialUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX__Terminals_Serial",
                table: "_Terminals",
                column: "Serial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__POSs_Serial",
                table: "_POSs",
                column: "Serial",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX__Terminals_Serial",
                table: "_Terminals");

            migrationBuilder.DropIndex(
                name: "IX__POSs_Serial",
                table: "_POSs");
        }
    }
}
