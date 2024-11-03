using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class NewRoleNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "Name",
                value: "Case Handler");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "Name",
                value: "Submitter");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89ddce13-89ef-4bd8-bb83-d077d1c0834c", "AQAAAAIAAYagAAAAEAQXgm491so8xEU1ufkecMXbewUxzjfullqwMyKKl5vIeflrSjwKBO3EA9mli4LY2Q==", "1ef9e84b-1ed1-4194-818d-47144e0acf53" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "Name",
                value: "Saksbehandler");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "Name",
                value: "Innmelder");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73036c92-dc05-4eae-9a8e-b7151910db9d", "AQAAAAIAAYagAAAAEDi5/WcmfijD9f+U4fNvG44d/uqzbHUQg717ygcJXtQCrwPOYv4hi53MD9eDbuCLAA==", "f27f51c9-3172-46e6-aee0-3abb0e8e118b" });
        }
    }
}
