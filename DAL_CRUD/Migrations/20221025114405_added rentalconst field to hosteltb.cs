using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL_CRUD.Migrations
{
    public partial class addedrentalconstfieldtohosteltb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RentalCost",
                table: "Hostels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalCost",
                table: "Hostels");
        }
    }
}
