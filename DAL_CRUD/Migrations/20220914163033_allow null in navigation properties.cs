using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL_CRUD.Migrations
{
    public partial class allownullinnavigationproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_Hostels_HostelId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Hostels_HostelId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameIndex(
                name: "IX_books_HostelId",
                table: "Books",
                newName: "IX_Books_HostelId");

            migrationBuilder.AlterColumn<int>(
                name: "HostelId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HostelId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Hostels_HostelId",
                table: "Books",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Hostels_HostelId",
                table: "Users",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Hostels_HostelId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Hostels_HostelId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameIndex(
                name: "IX_Books_HostelId",
                table: "books",
                newName: "IX_books_HostelId");

            migrationBuilder.AlterColumn<int>(
                name: "HostelId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HostelId",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_books_Hostels_HostelId",
                table: "books",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Hostels_HostelId",
                table: "Users",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
