using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KartverketProsjekt.Migrations
{
    /// <inheritdoc />
    public partial class ResolvedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_MapReport_MapReportModelMapReportId",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_MapReportModelMapReportId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "MapReportModelMapReportId",
                table: "Attachment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapReportModelMapReportId",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_MapReportModelMapReportId",
                table: "Attachment",
                column: "MapReportModelMapReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_MapReport_MapReportModelMapReportId",
                table: "Attachment",
                column: "MapReportModelMapReportId",
                principalTable: "MapReport",
                principalColumn: "MapReportId");
        }
    }
}
