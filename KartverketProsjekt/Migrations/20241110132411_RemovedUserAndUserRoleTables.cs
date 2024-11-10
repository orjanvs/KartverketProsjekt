using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserAndUserRoleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48bd0d95-86fe-43d2-9f17-4a4a311b8a88", "AQAAAAIAAYagAAAAEGErD1ZoF5M3921PdPYj7pC+ibv14rUdvomr5/sBniEAfFL+HB9D/A1KCKMcJrXTgw==", "62c1b309-80a9-4d22-a93e-32543fc6b478" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1e27036-e4eb-47b9-8c22-96b8d2fdd0bd", "AQAAAAIAAYagAAAAEEy+QIMuMHmXzw9NcjZmCGeh/LDfCnyiDJQeIrPOi6VeDEOMSr5pQokmTI1e86HvwA==", "91626fec-2932-4892-8574-4ec84e4a549c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81a86691-cf20-417e-884d-509a43b8360a", "AQAAAAIAAYagAAAAEOnm3CdnVbC7eodEWeRL4qn3pVDGoYOvu6OTyenxVFT3lnDT3yk/HLwUtedutM4i3A==", "6f603c29-78fe-47d2-ba0e-2916b21af2b8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d040f1a-abbc-46ad-96d8-784569ea1b74", "AQAAAAIAAYagAAAAEAaXZPGr0WCbdz1Y4oKp8VBpST8fiSg9fRlehxLl6fJOyUfgD0qN57Rbb3y/63l7xw==", "d602cb13-31e6-4b22-a647-cd5f46261ce8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "871990ae-b26c-439f-94ee-9fa4bccecdec", "AQAAAAIAAYagAAAAEBX0FgnvxAuHnjPLG3Q2e7+Y6Jy5hn4FJ0HwmUb41OleCfplez4l11U78wGe4qJ4Ww==", "662297a3-4b77-4d28-b7e2-86595436a89c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f111744-7c39-45cb-9e34-9cdffcb7b8e2", "AQAAAAIAAYagAAAAEJFI5hFKd7v8qYWBhvNctZiz8LK2Zg80wTqKHhUbQLByKEEBeoST+6gRC0LhRcWpzg==", "a9742cfd-b5aa-4c04-8c53-85dd7c1f3a68" });
        }
    }
}
