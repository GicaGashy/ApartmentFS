using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AptOnly.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    StreetName1 = table.Column<string>(nullable: true),
                    StreetName2 = table.Column<string>(nullable: true),
                    VillageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    ApartmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Floor = table.Column<int>(nullable: false),
                    Bedroom = table.Column<int>(nullable: false),
                    BathRoom = table.Column<int>(nullable: false),
                    PricePerMonth = table.Column<decimal>(nullable: false),
                    IsFurbished = table.Column<bool>(nullable: true),
                    M2 = table.Column<decimal>(nullable: false),
                    PricePerM2 = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.ApartmentId);
                    table.ForeignKey(
                        name: "FK_Apartments_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apartments_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apartments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CityName" },
                values: new object[,]
                {
                    { 1, "Artane" },
                    { 31, "Viti" },
                    { 30, "Therandë" },
                    { 29, "Shtime" },
                    { 28, "Shtërpcë" },
                    { 27, "Skenderaj" },
                    { 26, "Skenderaj" },
                    { 25, "Rahovec" },
                    { 24, "Prizren" },
                    { 23, "Prishtinë" },
                    { 22, "Prishtinë" },
                    { 21, "Prishtinë" },
                    { 20, "Prishtinë" },
                    { 19, "Pejë" },
                    { 18, "Mitrovicë" },
                    { 32, "Zubin Potok" },
                    { 17, "Malishevë" },
                    { 15, "Leposaviq" },
                    { 14, "Klinë" },
                    { 13, "Kaçanik" },
                    { 12, "Kastriot" },
                    { 11, "Gjilan" },
                    { 10, "Gjakovë" },
                    { 9, "Fushë Kosovë" },
                    { 8, "Ferizaj" },
                    { 7, "Drenas" },
                    { 6, "Dragash" },
                    { 5, "Deçan" },
                    { 4, "Dardane" },
                    { 3, "Burim" },
                    { 2, "Besiane" },
                    { 16, "Lipjan" },
                    { 33, "Zveçan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_AddressId",
                table: "Apartments",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_StatusId",
                table: "Apartments",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_UserId",
                table: "Apartments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
