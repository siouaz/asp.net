using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OeuilDeSauron.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMailBoolToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SendMailIfUnhealthy",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendSMSIfUnhealthy",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendTeamsNotificationIfUnhealthy",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendMailIfUnhealthy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SendSMSIfUnhealthy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SendTeamsNotificationIfUnhealthy",
                table: "Projects");
        }
    }
}
