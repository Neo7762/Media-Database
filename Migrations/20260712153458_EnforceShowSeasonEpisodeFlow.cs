using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media_Database.Migrations
{
    /// <inheritdoc />
    public partial class EnforceShowSeasonEpisodeFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Seasons_SeasonId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Shows_ShowId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Seasons_SeasonId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Shows_ShowId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Shows_ShowId",
                table: "Seasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Writers_Seasons_SeasonId",
                table: "Writers");

            migrationBuilder.DropForeignKey(
                name: "FK_Writers_Shows_ShowId",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Writers_SeasonId",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Writers_ShowId",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Directors_SeasonId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Directors_ShowId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_SeasonId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_ShowId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Actors");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShowId",
                table: "Seasons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Shows_ShowId",
                table: "Seasons",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Shows_ShowId",
                table: "Seasons");

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonId",
                table: "Writers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShowId",
                table: "Writers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ShowId",
                table: "Seasons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonId",
                table: "Directors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShowId",
                table: "Directors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonId",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShowId",
                table: "Actors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Writers_SeasonId",
                table: "Writers",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Writers_ShowId",
                table: "Writers",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_SeasonId",
                table: "Directors",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_ShowId",
                table: "Directors",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_SeasonId",
                table: "Actors",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ShowId",
                table: "Actors",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Seasons_SeasonId",
                table: "Actors",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Shows_ShowId",
                table: "Actors",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Seasons_SeasonId",
                table: "Directors",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Shows_ShowId",
                table: "Directors",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Shows_ShowId",
                table: "Seasons",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_Seasons_SeasonId",
                table: "Writers",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_Shows_ShowId",
                table: "Writers",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id");
        }
    }
}
