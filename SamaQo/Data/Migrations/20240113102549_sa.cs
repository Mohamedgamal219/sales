using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamaQo.Data.Migrations
{
    /// <inheritdoc />
    public partial class sa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesSheetId",
                table: "SalesSheetDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesSheetDetails_SalesSheetId",
                table: "SalesSheetDetails",
                column: "SalesSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesSheetDetails_SalesSheets_SalesSheetId",
                table: "SalesSheetDetails",
                column: "SalesSheetId",
                principalTable: "SalesSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesSheetDetails_SalesSheets_SalesSheetId",
                table: "SalesSheetDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalesSheetDetails_SalesSheetId",
                table: "SalesSheetDetails");

            migrationBuilder.DropColumn(
                name: "SalesSheetId",
                table: "SalesSheetDetails");
        }
    }
}
