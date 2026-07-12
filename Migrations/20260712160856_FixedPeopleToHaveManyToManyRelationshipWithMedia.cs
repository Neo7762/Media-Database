using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media_Database.Migrations
{
    /// <inheritdoc />
    public partial class FixedPeopleToHaveManyToManyRelationshipWithMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Episodes_EpisodeId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_MovieId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Episodes_EpisodeId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Movies_MovieId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_Writers_Episodes_EpisodeId",
                table: "Writers");

            migrationBuilder.DropForeignKey(
                name: "FK_Writers_Movies_MovieId",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Writers_EpisodeId",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Writers_MovieId",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Directors_EpisodeId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Directors_MovieId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_EpisodeId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_MovieId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "EpisodeId",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "EpisodeId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "EpisodeId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Actors");

            migrationBuilder.AddColumn<int>(
                name: "SeasonNumber",
                table: "Seasons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ActorEpisode",
                columns: table => new
                {
                    ActorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EpisodesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorEpisode", x => new { x.ActorsId, x.EpisodesId });
                    table.ForeignKey(
                        name: "FK_ActorEpisode_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorEpisode_Episodes_EpisodesId",
                        column: x => x.EpisodesId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectorEpisode",
                columns: table => new
                {
                    DirectorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EpisodesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorEpisode", x => new { x.DirectorsId, x.EpisodesId });
                    table.ForeignKey(
                        name: "FK_DirectorEpisode_Directors_DirectorsId",
                        column: x => x.DirectorsId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorEpisode_Episodes_EpisodesId",
                        column: x => x.EpisodesId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectorMovie",
                columns: table => new
                {
                    DirectorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorMovie", x => new { x.DirectorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_DirectorMovie_Directors_DirectorsId",
                        column: x => x.DirectorsId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeWriter",
                columns: table => new
                {
                    EpisodesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WritersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeWriter", x => new { x.EpisodesId, x.WritersId });
                    table.ForeignKey(
                        name: "FK_EpisodeWriter_Episodes_EpisodesId",
                        column: x => x.EpisodesId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EpisodeWriter_Writers_WritersId",
                        column: x => x.WritersId,
                        principalTable: "Writers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieWriter",
                columns: table => new
                {
                    MoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WritersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieWriter", x => new { x.MoviesId, x.WritersId });
                    table.ForeignKey(
                        name: "FK_MovieWriter_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieWriter_Writers_WritersId",
                        column: x => x.WritersId,
                        principalTable: "Writers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorEpisode_EpisodesId",
                table: "ActorEpisode",
                column: "EpisodesId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesId",
                table: "ActorMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorEpisode_EpisodesId",
                table: "DirectorEpisode",
                column: "EpisodesId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorMovie_MoviesId",
                table: "DirectorMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeWriter_WritersId",
                table: "EpisodeWriter",
                column: "WritersId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieWriter_WritersId",
                table: "MovieWriter",
                column: "WritersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorEpisode");

            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.DropTable(
                name: "DirectorEpisode");

            migrationBuilder.DropTable(
                name: "DirectorMovie");

            migrationBuilder.DropTable(
                name: "EpisodeWriter");

            migrationBuilder.DropTable(
                name: "MovieWriter");

            migrationBuilder.DropColumn(
                name: "SeasonNumber",
                table: "Seasons");

            migrationBuilder.AddColumn<Guid>(
                name: "EpisodeId",
                table: "Writers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "Writers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EpisodeId",
                table: "Directors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "Directors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EpisodeId",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Writers_EpisodeId",
                table: "Writers",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Writers_MovieId",
                table: "Writers",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_EpisodeId",
                table: "Directors",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_MovieId",
                table: "Directors",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_EpisodeId",
                table: "Actors",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MovieId",
                table: "Actors",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Episodes_EpisodeId",
                table: "Actors",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_MovieId",
                table: "Actors",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Episodes_EpisodeId",
                table: "Directors",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Movies_MovieId",
                table: "Directors",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_Episodes_EpisodeId",
                table: "Writers",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_Movies_MovieId",
                table: "Writers",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
