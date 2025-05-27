using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace football_league.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTeamLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                schema: "FootballLeagueApp",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                schema: "FootballLeagueApp",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
