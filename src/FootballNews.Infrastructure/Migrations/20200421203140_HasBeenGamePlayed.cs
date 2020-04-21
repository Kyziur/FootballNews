using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballNews.Infrastructure.Migrations
{
    public partial class HasBeenGamePlayed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenPlayed",
                table: "Games",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenPlayed",
                table: "Games");
        }
    }
}
