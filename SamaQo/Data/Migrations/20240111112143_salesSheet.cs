using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamaQo.Data.Migrations
{
    /// <inheritdoc />
    public partial class salesSheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesSheetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    SalesSheetId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesSheetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesSheetDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesSheetDetails_SalesSheets_SalesSheetId",
                        column: x => x.SalesSheetId,
                        principalTable: "SalesSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesSheetDetails_ProductId",
                table: "SalesSheetDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesSheetDetails_SalesSheetId",
                table: "SalesSheetDetails",
                column: "SalesSheetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesSheetDetails");

            migrationBuilder.DropTable(
                name: "SalesSheets");
        }
    }
}
