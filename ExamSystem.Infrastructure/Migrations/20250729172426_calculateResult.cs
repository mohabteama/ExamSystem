using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class calculateResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "ExamResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_SubjectId",
                table: "ExamResults",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Subjects_SubjectId",
                table: "ExamResults",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Subjects_SubjectId",
                table: "ExamResults");

            migrationBuilder.DropIndex(
                name: "IX_ExamResults_SubjectId",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ExamResults");
        }
    }
}
