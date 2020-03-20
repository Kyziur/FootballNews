using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballNews.Infrastructure.Migrations
{
    public partial class AddedSeedForRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6307dd08-682a-4518-add3-5d2a1ee86fe1", "7f8a139e-f97c-4870-aef6-7c3339bd24b9", "Admin", null },
                    { "c4ea9cdf-b76c-4a78-ac12-37cb7771fd9d", "17e0f30c-8cae-49fc-91a9-8af60700340d", "Editor", null },
                    { "4144df13-24bb-413a-a6d6-0972f6f58fbb", "47f56783-547d-4e4b-b43c-d7e1fdfb1e47", "User", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");
        }
    }
}
