using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class InternetNetworks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternetNetworks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkName3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkName4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkName5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternetNetworks", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternetNetworks");
        }
    }
}
