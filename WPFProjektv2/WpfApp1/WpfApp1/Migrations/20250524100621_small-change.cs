using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp1.Migrations
{
    /// <inheritdoc />
    public partial class smallchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "Charakters");

            migrationBuilder.RenameIndex(
                name: "IX_Charakter_PlayerId",
                table: "Charakters",
                newName: "IX_Charakters_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charakters",
                table: "Charakters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Charakters_Players_PlayerId",
                table: "Charakters",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rolls_Charakters_CharakterId",
                table: "Rolls",
                column: "CharakterId",
                principalTable: "Charakters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charakters_Players_PlayerId",
                table: "Charakters");

            migrationBuilder.DropForeignKey(
                name: "FK_Rolls_Charakters_CharakterId",
                table: "Rolls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charakters",
                table: "Charakters");

            migrationBuilder.RenameTable(
                name: "Charakters",
                newName: "Charakter");

            migrationBuilder.RenameIndex(
                name: "IX_Charakters_PlayerId",
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
    }
}
