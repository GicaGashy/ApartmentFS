using Microsoft.EntityFrameworkCore.Migrations;

namespace AptOnly.Data.Migrations
{
    public partial class IsRentingAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRenting",
                table: "Apartments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRenting",
                table: "Apartments");
        }
    }
}
