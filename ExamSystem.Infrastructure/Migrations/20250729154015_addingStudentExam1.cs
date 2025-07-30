using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingStudentExam1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExam",
                table: "StudentExam");

            migrationBuilder.DropIndex(
                name: "IX_StudentExam_StudentId",
                table: "StudentExam");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentExam");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExam",
                table: "StudentExam",
                columns: new[] { "StudentId", "ExamId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentExam",
                table: "StudentExam");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentExam",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentExam",
                table: "StudentExam",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_StudentId",
                table: "StudentExam",
                column: "StudentId");
        }
    }
}
