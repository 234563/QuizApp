using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class UserQuestionsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Questions_QuestionId",
                table: "ApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_QuestionId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "ApplicationUsers");

            migrationBuilder.CreateTable(
                name: "QuestionUser",
                columns: table => new
                {
                    QuestionsAnsweredId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoAnsweredId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionUser", x => new { x.QuestionsAnsweredId, x.UsersWhoAnsweredId });
                    table.ForeignKey(
                        name: "FK_QuestionUser_ApplicationUsers_UsersWhoAnsweredId",
                        column: x => x.UsersWhoAnsweredId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionUser_Questions_QuestionsAnsweredId",
                        column: x => x.QuestionsAnsweredId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionUser_UsersWhoAnsweredId",
                table: "QuestionUser",
                column: "UsersWhoAnsweredId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionUser");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "ApplicationUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_QuestionId",
                table: "ApplicationUsers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Questions_QuestionId",
                table: "ApplicationUsers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
