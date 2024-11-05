using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogue_User_RecipientId",
                table: "Dialogue");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogue_User_SenderId",
                table: "Dialogue");

            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_User_CaseHandlerId",
                table: "MapReport");

            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_User_SubmitterId",
                table: "MapReport");

            migrationBuilder.AlterColumn<string>(
                name: "SubmitterId",
                table: "MapReport",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CaseHandlerId",
                table: "MapReport",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Dialogue",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RecipientId",
                table: "Dialogue",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7bc41ab-6ce4-4621-90be-b79b8fe4b0a0", "AQAAAAIAAYagAAAAEIAzXshUDrSkhlv2n5yMGHp64pwJ9aWeuP2jmrQ/8RUIA50a1PHNHcVoHPu146WZvA==", "f8c8efd2-b21c-48a0-9b24-7e872b621114" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2", 0, "5ef76018-28b7-4a5b-aba0-4d310c989d08", "submitter@test.com", false, "Test", "Submitter", false, null, "SUBMITTER@TEST.COM", "SUBMITTER@TEST.COM", "AQAAAAIAAYagAAAAEJ7hPVLTxBPWRE9e1I6jrUBdUEamUT9cO0DryZXWh0Zu4NBJvkchradaqA/wIMevig==", null, false, "13e38be9-6b27-4785-9ae3-7fa01f5f5299", false, "submitter@test.com" },
                    { "3", 0, "f69c989f-5494-4e6c-8110-9b512d3e7315", "casehandler@test.com", false, "Test", "CaseHandler", false, null, "CASEHANDLER@TEST.COM", "CASEHANDLER@TEST.COM", "AQAAAAIAAYagAAAAEHpt7vaOnxRgFvUDAZhMpsEAOL3LZDILpz5BSpvwDzvirav1A/RJmGbQGsCPDy5WUg==", null, false, "371be20a-7d27-4a0e-b058-d195029af198", false, "casehandler@test.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3", "2" },
                    { "2", "3" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogue_AspNetUsers_RecipientId",
                table: "Dialogue",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogue_AspNetUsers_SenderId",
                table: "Dialogue",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogue_AspNetUsers_RecipientId",
                table: "Dialogue");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogue_AspNetUsers_SenderId",
                table: "Dialogue");

            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_AspNetUsers_CaseHandlerId",
                table: "MapReport");

            migrationBuilder.DropForeignKey(
                name: "FK_MapReport_AspNetUsers_SubmitterId",
                table: "MapReport");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "3" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.AlterColumn<int>(
                name: "SubmitterId",
                table: "MapReport",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CaseHandlerId",
                table: "MapReport",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Dialogue",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "RecipientId",
                table: "Dialogue",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db249c31-2c3a-4856-aef9-fc81189d96a6", "AQAAAAIAAYagAAAAEPEwdUhqjOMMkm/8CUSRyI8rFMrjwXK6FA0o/y0hTKC7r/1juUDryIGYchvk+G+SGw==", "a1c67a31-9aa7-4662-bf5d-24bc5618a40a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogue_User_RecipientId",
                table: "Dialogue",
                column: "RecipientId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogue_User_SenderId",
                table: "Dialogue",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MapReport_User_CaseHandlerId",
                table: "MapReport",
                column: "CaseHandlerId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MapReport_User_SubmitterId",
                table: "MapReport",
                column: "SubmitterId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
