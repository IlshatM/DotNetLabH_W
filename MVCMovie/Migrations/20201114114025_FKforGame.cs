using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMovie.Migrations
{
    public partial class FKforGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Game_MovieID",
                table: "Game",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Movie_MovieID",
                table: "Game",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Movie_MovieID",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_MovieID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "Game");

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
    }
}
