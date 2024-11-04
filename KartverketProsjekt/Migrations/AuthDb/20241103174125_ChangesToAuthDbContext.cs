using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class ChangesToAuthDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b9649340-61f8-423b-b6ba-e0a1927ae8f5", "AQAAAAIAAYagAAAAEB9VE9eXRboHl5TyPlK1fNLkwk5Is5vGPH1QdwslwEfrnLQNJWoMB9sG9foWBNFF5w==", "7a5cb95e-4f11-45a1-890c-55de184e074c", "sysadmin@test.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "c286bb56-4ed7-4f34-aacc-8c076f76d46e", "AQAAAAIAAYagAAAAEEgfRuzGAlxIfJ7T95oTHRpm2TyDCFKulDyio3ezWj49qJQh863ab3i1SkQOKfrYBA==", "d56b1a3f-79d1-4a7b-a263-651f17b84c0a", "sysadmin" });
        }
    }
}
