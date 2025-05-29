using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Charaktere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reputation = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Humanity = table.Column<int>(type: "int", nullable: false),
                    SkillsDBDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatsBDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charaktere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charaktere_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharakterId = table.Column<int>(type: "int", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Modifier = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    Dificulty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rolls_Charaktere_CharakterId",
                        column: x => x.CharakterId,
                        principalTable: "Charaktere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charaktere_PlayerId",
                table: "Charaktere",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rolls_CharakterId",
                table: "Rolls",
                column: "CharakterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rolls");

            migrationBuilder.DropTable(
                name: "Charaktere");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
