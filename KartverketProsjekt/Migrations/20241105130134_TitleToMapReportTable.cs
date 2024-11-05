using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class TitleToMapReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MapReport",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea76989c-3cc9-43eb-b253-f0e042643053", "AQAAAAIAAYagAAAAEJ4qWPVZOyce6xDaAAEAs4rOJZayW8JokQfCujlY3MS9IhYofFQ5WEhUFqpJ0nLMxQ==", "39b5c17c-c90d-4566-8ff9-d58bbd483e7f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50617e6e-f858-4d65-a9e7-db0547ba7ae1", "AQAAAAIAAYagAAAAEGmHyArl6eVG0ZYFZJkadOjk1nO7pFiaQmPQT8BWpuSbuobV84FKK6CtXs3mwHvbGw==", "a802a46d-694e-431c-ab95-be430b3bd579" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec8307a0-f855-4436-8853-e5c1763cebe8", "AQAAAAIAAYagAAAAEGWjDlUhWYp/+ZX3sAohTslM1uS6Z4oPCH8emo7bkrzbxnQy4lyjC6IsQ05kMp7BHQ==", "37910a97-a33c-4ec3-bbda-63142a44a20c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "MapReport");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c881107-7d78-4902-bc51-1fdeb8835983", "AQAAAAIAAYagAAAAEJjjWEqsGYeMElUU3sTTlkvC8JWPK6M6jIYr+0erD+aUjqgzL02MCWkBqY1HQGOAUw==", "051975a7-6269-4db2-803e-da16a083b078" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77b87f8c-fa23-45b0-b889-78db3c5ac7a8", "AQAAAAIAAYagAAAAEG4VCqaJy28FKa3O2IJqb6wSTZYh+XCdNXoTXCjCspsJlcAQHI/kAADC0VgwRtOzZg==", "bb3d682a-5913-45a1-87d4-6ae404135851" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dee1719-c42a-4e4b-80db-fda3fa2960c5", "AQAAAAIAAYagAAAAEDWXs6H+nCkbViEeBXMLjo0qJwzZcaj5SJpbl3plhcQU9tx8FTdrE6tesmLfBiPhGw==", "4926446b-49d6-48c6-af6f-a0fa53257fa4" });
        }
    }
}
