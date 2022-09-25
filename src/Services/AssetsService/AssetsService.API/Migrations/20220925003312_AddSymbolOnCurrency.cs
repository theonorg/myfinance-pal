using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetsService.API.Migrations
{
    public partial class AddSymbolOnCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "symbol",
                table: "currencies",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "currencies",
                keyColumn: "id",
                keyValue: 1,
                column: "symbol",
                value: "€");

            migrationBuilder.UpdateData(
                table: "currencies",
                keyColumn: "id",
                keyValue: 2,
                column: "symbol",
                value: "$");

            migrationBuilder.UpdateData(
                table: "currencies",
                keyColumn: "id",
                keyValue: 3,
                column: "symbol",
                value: "£");

            migrationBuilder.UpdateData(
                table: "currencies",
                keyColumn: "id",
                keyValue: 4,
                column: "symbol",
                value: "R$");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "symbol",
                table: "currencies");
        }
    }
}
