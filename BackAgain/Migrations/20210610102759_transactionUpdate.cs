using Microsoft.EntityFrameworkCore.Migrations;

namespace BackAgain.Migrations
{
    public partial class transactionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebSocketConnectionId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "_Transaction",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                table: "_OrderItem",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebSocketConnectionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "_Transaction");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                table: "_OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
