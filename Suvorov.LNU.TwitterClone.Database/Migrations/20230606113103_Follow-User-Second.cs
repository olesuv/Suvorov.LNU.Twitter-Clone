using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suvorov.LNU.TwitterClone.Database.Migrations
{
    /// <inheritdoc />
    public partial class FollowUserSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FollowId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Follow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuurentUserId = table.Column<int>(type: "int", nullable: true),
                    FollowersAmount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follow_User_CuurentUserId",
                        column: x => x.CuurentUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_FollowId",
                table: "User",
                column: "FollowId");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_CuurentUserId",
                table: "Follow",
                column: "CuurentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Follow_FollowId",
                table: "User",
                column: "FollowId",
                principalTable: "Follow",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Follow_FollowId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropIndex(
                name: "IX_User_FollowId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FollowId",
                table: "User");
        }
    }
}
