using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class FirstAndLastNameToIdentityUserV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73036c92-dc05-4eae-9a8e-b7151910db9d", "System", "Administrator", "AQAAAAIAAYagAAAAEDi5/WcmfijD9f+U4fNvG44d/uqzbHUQg717ygcJXtQCrwPOYv4hi53MD9eDbuCLAA==", "f27f51c9-3172-46e6-aee0-3abb0e8e118b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9649340-61f8-423b-b6ba-e0a1927ae8f5", "AQAAAAIAAYagAAAAEB9VE9eXRboHl5TyPlK1fNLkwk5Is5vGPH1QdwslwEfrnLQNJWoMB9sG9foWBNFF5w==", "7a5cb95e-4f11-45a1-890c-55de184e074c" });
        }
    }
}
