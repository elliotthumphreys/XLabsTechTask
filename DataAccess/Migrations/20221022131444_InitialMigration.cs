using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "dbo",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                schema: "dbo",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueUrl = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TwitterHandle = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Latitude = table.Column<double>(type: "float(9)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<double>(type: "float(8)", precision: 8, scale: 6, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    VenueType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "dbo",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeerRating = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    AtmosphereRating = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    AmenitiesRating = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    ValueForMoneyRating = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    DateOfReview = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Venues_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "dbo",
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueTags",
                schema: "dbo",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueTags", x => new { x.VenueId, x.TagId });
                    table.ForeignKey(
                        name: "FK_VenueTags_Tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "dbo",
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenueTags_Venues_VenueId",
                        column: x => x.VenueId,
                        principalSchema: "dbo",
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_VenueId",
                schema: "dbo",
                table: "Reviews",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueTags_TagId",
                schema: "dbo",
                table: "VenueTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "VenueTags",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Venues",
                schema: "dbo");
        }
    }
}
