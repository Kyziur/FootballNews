using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballNews.Infrastructure.Migrations
{
    public partial class AddedMissingRelatinForTeamsInGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Teams_TeamId1",
                table: "Goal");

            migrationBuilder.DropIndex(
                name: "IX_Goal_TeamId1",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "TeamId1",
                table: "Goal");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Goal",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShooterId",
                table: "Goal",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Time",
                table: "Goal",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Report = table.Column<string>(nullable: true),
                    HomeTeamId = table.Column<Guid>(nullable: false),
                    AwayTeamId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goal_GameId",
                table: "Goal",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_ShooterId",
                table: "Goal",
                column: "ShooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                column: "HomeTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Games_GameId",
                table: "Goal",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Players_ShooterId",
                table: "Goal",
                column: "ShooterId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Games_GameId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Players_ShooterId",
                table: "Goal");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Goal_GameId",
                table: "Goal");

            migrationBuilder.DropIndex(
                name: "IX_Goal_ShooterId",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "ShooterId",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Goal");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId1",
                table: "Goal",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goal_TeamId1",
                table: "Goal",
                column: "TeamId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Teams_TeamId1",
                table: "Goal",
                column: "TeamId1",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
