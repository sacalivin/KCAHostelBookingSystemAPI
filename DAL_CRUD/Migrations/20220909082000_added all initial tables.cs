using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL_CRUD.Migrations
{
    public partial class addedallinitialtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArmenityRentAlternative_Armenity_ArmenitiesId",
                table: "ArmenityRentAlternative");

            migrationBuilder.DropForeignKey(
                name: "FK_ArmenityRentAlternative_RentAlternative_RentAlternativesId",
                table: "ArmenityRentAlternative");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Hostels_HostelId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_RentAlternative_Hostels_HostelId",
                table: "RentAlternative");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Hostels_HostelId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentAlternative",
                table: "RentAlternative");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Armenity",
                table: "Armenity");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "RentAlternative",
                newName: "RentAlternatives");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "books");

            migrationBuilder.RenameTable(
                name: "Armenity",
                newName: "Armenities");

            migrationBuilder.RenameIndex(
                name: "IX_User_HostelId",
                table: "Users",
                newName: "IX_Users_HostelId");

            migrationBuilder.RenameIndex(
                name: "IX_RentAlternative_HostelId",
                table: "RentAlternatives",
                newName: "IX_RentAlternatives_HostelId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_HostelId",
                table: "books",
                newName: "IX_books_HostelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentAlternatives",
                table: "RentAlternatives",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Armenities",
                table: "Armenities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArmenityRentAlternative_Armenities_ArmenitiesId",
                table: "ArmenityRentAlternative",
                column: "ArmenitiesId",
                principalTable: "Armenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArmenityRentAlternative_RentAlternatives_RentAlternativesId",
                table: "ArmenityRentAlternative",
                column: "RentAlternativesId",
                principalTable: "RentAlternatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_Hostels_HostelId",
                table: "books",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentAlternatives_Hostels_HostelId",
                table: "RentAlternatives",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArmenityRentAlternative_Armenities_ArmenitiesId",
                table: "ArmenityRentAlternative");

            migrationBuilder.DropForeignKey(
                name: "FK_ArmenityRentAlternative_RentAlternatives_RentAlternativesId",
                table: "ArmenityRentAlternative");

            migrationBuilder.DropForeignKey(
                name: "FK_books_Hostels_HostelId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_RentAlternatives_Hostels_HostelId",
                table: "RentAlternatives");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Hostels_HostelId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentAlternatives",
                table: "RentAlternatives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Armenities",
                table: "Armenities");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "RentAlternatives",
                newName: "RentAlternative");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Book");

            migrationBuilder.RenameTable(
                name: "Armenities",
                newName: "Armenity");

            migrationBuilder.RenameIndex(
                name: "IX_Users_HostelId",
                table: "User",
                newName: "IX_User_HostelId");

            migrationBuilder.RenameIndex(
                name: "IX_RentAlternatives_HostelId",
                table: "RentAlternative",
                newName: "IX_RentAlternative_HostelId");

            migrationBuilder.RenameIndex(
                name: "IX_books_HostelId",
                table: "Book",
                newName: "IX_Book_HostelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentAlternative",
                table: "RentAlternative",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Armenity",
                table: "Armenity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArmenityRentAlternative_Armenity_ArmenitiesId",
                table: "ArmenityRentAlternative",
                column: "ArmenitiesId",
                principalTable: "Armenity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArmenityRentAlternative_RentAlternative_RentAlternativesId",
                table: "ArmenityRentAlternative",
                column: "RentAlternativesId",
                principalTable: "RentAlternative",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Hostels_HostelId",
                table: "Book",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentAlternative_Hostels_HostelId",
                table: "RentAlternative",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Hostels_HostelId",
                table: "User",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
