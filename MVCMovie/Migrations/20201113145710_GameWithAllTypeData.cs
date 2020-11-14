using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMovie.Migrations
{
    public partial class GameWithAllTypeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CrossPlatformMultiplayer",
                table: "Game",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Perspective",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TotalHours",
                table: "Game",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CrossPlatformMultiplayer",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Perspective",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Game",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
