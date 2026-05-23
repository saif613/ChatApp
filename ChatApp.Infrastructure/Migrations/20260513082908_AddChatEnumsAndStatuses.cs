using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChatEnumsAndStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserConnections",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Online");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Messages",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Sent");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Conversations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserConnections");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Conversations");
        }
    }
}
