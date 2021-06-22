using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackAgain.Migrations
{
    public partial class settingsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__OrderItem__ItemOption_MyPropertyId",
                table: "_OrderItem");

            migrationBuilder.DropIndex(
                name: "IX__UserSettings_UserId",
                table: "_UserSettings");

            migrationBuilder.DropIndex(
                name: "IX__OrderItem_MyPropertyId",
                table: "_OrderItem");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "_Orderstatus");

            migrationBuilder.DropColumn(
                name: "MyPropertyId",
                table: "_OrderItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "_Orderstatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "ItemOptionId",
                table: "_OrderItem",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TerminalSerial",
                table: "_Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__UserSettings_UserId",
                table: "_UserSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItem_ItemOptionId",
                table: "_OrderItem",
                column: "ItemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX__Order_TerminalSerial",
                table: "_Order",
                column: "TerminalSerial");

            migrationBuilder.AddForeignKey(
                name: "FK__Order__Terminals_TerminalSerial",
                table: "_Order",
                column: "TerminalSerial",
                principalTable: "_Terminals",
                principalColumn: "Serial",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderItem__ItemOption_ItemOptionId",
                table: "_OrderItem",
                column: "ItemOptionId",
                principalTable: "_ItemOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Order__Terminals_TerminalSerial",
                table: "_Order");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderItem__ItemOption_ItemOptionId",
                table: "_OrderItem");

            migrationBuilder.DropIndex(
                name: "IX__UserSettings_UserId",
                table: "_UserSettings");

            migrationBuilder.DropIndex(
                name: "IX__OrderItem_ItemOptionId",
                table: "_OrderItem");

            migrationBuilder.DropIndex(
                name: "IX__Order_TerminalSerial",
                table: "_Order");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "_Orderstatus");

            migrationBuilder.DropColumn(
                name: "TerminalSerial",
                table: "_Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "MyProperty",
                table: "_Orderstatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "ItemOptionId",
                table: "_OrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MyPropertyId",
                table: "_OrderItem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__UserSettings_UserId",
                table: "_UserSettings",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItem_MyPropertyId",
                table: "_OrderItem",
                column: "MyPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderItem__ItemOption_MyPropertyId",
                table: "_OrderItem",
                column: "MyPropertyId",
                principalTable: "_ItemOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
