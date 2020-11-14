using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMovie.Migrations
{
    public partial class MovieInGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasedOnGameMovieId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_BasedOnGameMovieId",
                table: "Game",
                column: "BasedOnGameMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Movie_BasedOnGameMovieId",
                table: "Game",
                column: "BasedOnGameMovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Movie_BasedOnGameMovieId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_BasedOnGameMovieId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "BasedOnGameMovieId",
                table: "Game");
        }
    }
}
