using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class CountyAndMunicipalityColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "MapReport",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "MapReport",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "County",
                table: "MapReport");

            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "MapReport");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsPrioritised = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserRoleName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_UserRole_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a0ac344-55ea-4b26-865e-cf51d2c71099", "AQAAAAIAAYagAAAAEN/tw2R9MpSQYdqBWJFRo+9LZ1o94gTCPRguLPPKBcU7uxgmf/QKz2L48qAGZF4lVg==", "b2ad9bb9-e210-4555-9daa-8c85cc4cfcfe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99018e10-7d1a-4d9d-927f-a419e2a4a2ef", "AQAAAAIAAYagAAAAEPCVmyEscFJBfNj+iWeo9HVpcifukpkeHOBtc6LgFW9vs27HKmvLftNz4WdfTQy23A==", "e4fe823d-24c0-403c-823e-9279f1aeed33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f5e9cde-929e-42a9-973b-97b030c646b0", "AQAAAAIAAYagAAAAEB0weVk8qQIAjvORv+BBIldh/wo9/OLo9xTfAhzHTof5DvEkssA/u33HvKGPfZr3wQ==", "6550ea73-3bd8-438c-8c36-9b2fc3813b6d" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "IsPrioritised", "UserRoleName" },
                values: new object[,]
                {
                    { 1, false, "Systemadministrator" },
                    { 2, false, "Saksbehandler" },
                    { 3, false, "Innmelder" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PhoneNumber", "UserRoleId" },
                values: new object[,]
                {
                    { 1, "default@example.com", "Default", "User", "0000000000", 2 },
                    { 2, "submitter@example.com", "Test", "Submitter", "1100000000", 3 },
                    { 3, "casehandler@example.com", "Test", "CaseHandler", "1200000000", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                table: "User",
                column: "UserRoleId");
        }
    }
}
