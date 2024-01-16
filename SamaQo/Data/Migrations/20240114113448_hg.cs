using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamaQo.Data.Migrations
{
    /// <inheritdoc />
    public partial class hg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesSheetDetails_SalesSheets_SalesSheetId",
                table: "SalesSheetDetails");

            migrationBuilder.DropTable(
                name: "SalesSheets");

            migrationBuilder.DropIndex(
                name: "IX_SalesSheetDetails_SalesSheetId",
                table: "SalesSheetDetails");

            migrationBuilder.DropColumn(
                name: "SalesSheetId",
                table: "SalesSheetDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesSheetId",
                table: "SalesSheetDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
