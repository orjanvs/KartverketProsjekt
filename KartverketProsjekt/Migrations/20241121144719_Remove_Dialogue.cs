using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Dialogue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dialogue");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b9df8c4-f6ec-4bee-8b25-c57cd8809b88", "AQAAAAIAAYagAAAAEJHWcZeV9numF/2jTYLh2LHe+MHx1GhH/dTBEsJmMLBgJt0FsMwnZWCyqwHqunXFWg==", "eaf9338c-6db5-478a-bf40-d00dd9d4f0c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb9aaee8-818e-4a8c-8ef9-f6f1a1dd1cfe", "AQAAAAIAAYagAAAAEGHPCa11BvSIEW1QkFc6V/rBmx8xYn/Nt+i213Po1N+SIKEYI9ZzUVBLJa4dHRgeIA==", "26a035e0-742b-4e82-bbbc-79c7a0479d67" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ebeddda-c163-4872-99e2-b0c57214e742", "AQAAAAIAAYagAAAAECMKOyVfah4vQkMKHwq0G8ZD1GR5NaQjpAo8upc0btxVoAwEvVNNIrDT4tUGMGZ7Cg==", "c4285ffe-0115-4ae5-bcd2-ffc1bf717622" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dialogue",
                columns: table => new
                {
                    DialogueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MapReportId = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SenderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialogue", x => x.DialogueId);
                    table.ForeignKey(
                        name: "FK_Dialogue_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dialogue_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dialogue_MapReport_MapReportId",
                        column: x => x.MapReportId,
                        principalTable: "MapReport",
                        principalColumn: "MapReportId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Dialogue_MapReportId",
                table: "Dialogue",
                column: "MapReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogue_RecipientId",
                table: "Dialogue",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogue_SenderId",
                table: "Dialogue",
                column: "SenderId");
        }
    }
}
