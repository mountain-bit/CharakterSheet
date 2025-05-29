using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp1.Migrations
{
    /// <inheritdoc />
    public partial class Rollupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Rolls",
                newName: "RolledValue");

            migrationBuilder.AddColumn<int>(
                name: "BaseValue",
                table: "Rolls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rolls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseValue",
                table: "Rolls");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rolls");

            migrationBuilder.RenameColumn(
                name: "RolledValue",
                table: "Rolls",
                newName: "Value");
        }
    }
}
