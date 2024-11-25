using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviourChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_AspNetUsers_CaseHandlerId",
                table: "MapReport");

            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_AspNetUsers_SubmitterId",
                table: "MapReport");

            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_MapReportStatus_MapReportStatusId",
                table: "MapReport");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MapReport",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "SubmitterId",
                table: "MapReport",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MapReport",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "614e8a9e-2d90-4d6e-b632-86c77263854a", "AQAAAAIAAYagAAAAENNom0dpnqT4LDfhtqex2zvwTH89BzPp2QQt6/ETinVn8y/eKC5yN8cPOvKkfz/Iiw==", "8ec91586-7126-4375-bfca-8f52e67d3f57" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4837a0c2-b727-4c81-bd91-6dcc4568bb61", "AQAAAAIAAYagAAAAEE7p70Wi7QJXYnC88YUmW9lr7ufsxbffNJd773rOeT3khn0Gj/NjVyLS+BaOkjt2Iw==", "25d763e7-432e-4a77-85ad-a69904ae3a48" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c55141a2-e732-42fe-b02a-d1061f34b3ce", "AQAAAAIAAYagAAAAECZ33Y5f+kw0c5do378h1H+Dx6SNkPTtdjsq9bUHyrmUQiw0j7ZGAp+aiSjx9e7j0g==", "91084da7-052a-4d84-958e-7ece618b887f" });

            migrationBuilder.AddForeignKey(
                name: "FK_MapReport_AspNetUsers_CaseHandlerId",
                table: "MapReport",
                column: "CaseHandlerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MapReport_AspNetUsers_SubmitterId",
                table: "MapReport",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MapReport_MapReportStatus_MapReportStatusId",
                table: "MapReport",
                column: "MapReportStatusId",
                principalTable: "MapReportStatus",
                principalColumn: "MapReportStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_AspNetUsers_CaseHandlerId",
                table: "MapReport");

            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_AspNetUsers_SubmitterId",
                table: "MapReport");

            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_MapReportStatus_MapReportStatusId",
                table: "MapReport");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MapReport",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "MapReport",
                keyColumn: "SubmitterId",
                keyValue: null,
                column: "SubmitterId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SubmitterId",
                table: "MapReport",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MapReport",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MapReport_AspNetUsers_CaseHandlerId",
                table: "MapReport",
                column: "CaseHandlerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MapReport_AspNetUsers_SubmitterId",
                table: "MapReport",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MapReport_MapReportStatus_MapReportStatusId",
                table: "MapReport",
                column: "MapReportStatusId",
                principalTable: "MapReportStatus",
                principalColumn: "MapReportStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
