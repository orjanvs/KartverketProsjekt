using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class NewSeedDataForMapLayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MapLayer",
                keyColumn: "MapLayerId",
                keyValue: 1,
                column: "MapLayerType",
                value: "Fargekart");

            migrationBuilder.UpdateData(
                table: "MapLayer",
                keyColumn: "MapLayerId",
                keyValue: 2,
                column: "MapLayerType",
                value: "Gråtonekart");

            migrationBuilder.UpdateData(
                table: "MapLayer",
                keyColumn: "MapLayerId",
                keyValue: 3,
                column: "MapLayerType",
                value: "Turkart");

            migrationBuilder.InsertData(
                table: "MapLayer",
                columns: new[] { "MapLayerId", "MapLayerType" },
                values: new object[] { 4, "Sjøkart" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MapLayer",
                keyColumn: "MapLayerId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "MapLayer",
                keyColumn: "MapLayerId",
                keyValue: 1,
                column: "MapLayerType",
                value: "Turkart");

            migrationBuilder.UpdateData(
                table: "MapLayer",
                keyColumn: "MapLayerId",
                keyValue: 2,
                column: "MapLayerType",
                value: "Sjøkart");

            migrationBuilder.UpdateData(
                table: "MapLayer",
                keyColumn: "MapLayerId",
                keyValue: 3,
                column: "MapLayerType",
                value: "Gråtonekart");
        }
    }
}
