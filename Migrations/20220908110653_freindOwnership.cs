using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FriendList.Migrations
{
    public partial class freindOwnership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Friend",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Friend");
        }
    }
}
