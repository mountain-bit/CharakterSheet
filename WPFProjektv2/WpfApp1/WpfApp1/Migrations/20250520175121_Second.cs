using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp1.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charaktere_Players_PlayerId",
                table: "Charaktere");

            migrationBuilder.DropForeignKey(
                name: "FK_Rolls_Charaktere_CharakterId",
                table: "Rolls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charaktere",
                table: "Charaktere");

            migrationBuilder.RenameTable(
                name: "Charaktere",
                newName: "Charakter");

            migrationBuilder.RenameIndex(
                name: "IX_Charaktere_PlayerId",
                table: "Charakter",
                newName: "IX_Charakter_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charakter",
                table: "Charakter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Charakter_Players_PlayerId",
                table: "Charakter",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rolls_Charakter_CharakterId",
                table: "Rolls",
                column: "CharakterId",
                principalTable: "Charakter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charakter_Players_PlayerId",
                table: "Charakter");

            migrationBuilder.DropForeignKey(
                name: "FK_Rolls_Charakter_CharakterId",
                table: "Rolls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charakter",
                table: "Charakter");

            migrationBuilder.RenameTable(
                name: "Charakter",
                newName: "Charaktere");

            migrationBuilder.RenameIndex(
                name: "IX_Charakter_PlayerId",
                table: "Charaktere",
                newName: "IX_Charaktere_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charaktere",
                table: "Charaktere",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Charaktere_Players_PlayerId",
                table: "Charaktere",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rolls_Charaktere_CharakterId",
                table: "Rolls",
                column: "CharakterId",
                principalTable: "Charaktere",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
