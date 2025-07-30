using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editOnStudentAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Options");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "Options",
                newName: "option");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "StudentAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "StudentAnswers");

            migrationBuilder.RenameColumn(
                name: "option",
                table: "Options",
                newName: "Answer");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Options",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
