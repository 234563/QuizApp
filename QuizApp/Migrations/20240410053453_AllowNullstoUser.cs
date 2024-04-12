using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullstoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "ApplicationUsers");
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "ApplicationUsers",
                nullable: true,
                defaultValue: ""
                );
            migrationBuilder.AlterColumn<string>(
               name: "DateOfBirth",
               table: "ApplicationUsers",
               nullable: true
               );
            migrationBuilder.AlterColumn<string>(
               name: "PhoneNumber",
               table: "ApplicationUsers",
               nullable: true
               );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "ApplicationUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
