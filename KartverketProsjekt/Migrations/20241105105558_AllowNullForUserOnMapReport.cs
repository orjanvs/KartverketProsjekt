using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullForUserOnMapReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CaseHandlerId",
                table: "MapReport",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MapReport",
                keyColumn: "CaseHandlerId",
                keyValue: null,
                column: "CaseHandlerId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CaseHandlerId",
                table: "MapReport",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da9109df-8101-458f-b170-97cd962088b9", "AQAAAAIAAYagAAAAEBzKOsVXxuTGi6fOk2xSqLb1OKOQyY8Fhe3lUaaEHn6V3IOvsEA4BPIh4tTEkC9C+g==", "eec2cd01-449f-4a1f-a7a8-f6ac1a417acc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fb22c4d-a16f-4e0b-8199-5057f660640b", "AQAAAAIAAYagAAAAEJ654uSIQ5bzkBmhDxfxvYpnXwHl92RxQg3jLUyRAmRPp629uzku17zwn836ZKYsGQ==", "ce828a3f-1fd7-40eb-ba7d-318533437bf0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b18a2842-7fe7-4beb-8677-c0279b6861a3", "AQAAAAIAAYagAAAAEDqNzrErmo4Bfmvzcp44//sgq4jf0936p/IVDxnc0pedbRlAG0nMijffLJAItBpFRQ==", "94447a7c-39bc-4bd5-98c7-5e323205522a" });
        }
    }
}
