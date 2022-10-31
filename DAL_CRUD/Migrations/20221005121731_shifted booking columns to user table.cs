using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL_CRUD.Migrations
{
    public partial class shiftedbookingcolumnstousertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentAlternatives_Hostels_HostelId",
                table: "RentAlternatives");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckinDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModeOfPayment",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Request",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HostelId",
                table: "RentAlternatives",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Hostels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_RentAlternatives_Hostels_HostelId",
                table: "RentAlternatives",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentAlternatives_Hostels_HostelId",
                table: "RentAlternatives");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CheckinDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModeOfPayment",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Request",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "HostelId",
                table: "RentAlternatives",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Hostels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentAlternatives_Hostels_HostelId",
                table: "RentAlternatives",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
