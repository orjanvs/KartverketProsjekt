using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestUsers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7bc41ab-6ce4-4621-90be-b79b8fe4b0a0", "AQAAAAIAAYagAAAAEIAzXshUDrSkhlv2n5yMGHp64pwJ9aWeuP2jmrQ/8RUIA50a1PHNHcVoHPu146WZvA==", "f8c8efd2-b21c-48a0-9b24-7e872b621114" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ef76018-28b7-4a5b-aba0-4d310c989d08", "AQAAAAIAAYagAAAAEJ7hPVLTxBPWRE9e1I6jrUBdUEamUT9cO0DryZXWh0Zu4NBJvkchradaqA/wIMevig==", "13e38be9-6b27-4785-9ae3-7fa01f5f5299" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f69c989f-5494-4e6c-8110-9b512d3e7315", "AQAAAAIAAYagAAAAEHpt7vaOnxRgFvUDAZhMpsEAOL3LZDILpz5BSpvwDzvirav1A/RJmGbQGsCPDy5WUg==", "371be20a-7d27-4a0e-b058-d195029af198" });
        }
    }
}
