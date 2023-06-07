using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suvorov.LNU.TwitterClone.Database.Migrations
{
    /// <inheritdoc />
    public partial class FolloweeInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FolloweeId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Followee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentUserId = table.Column<int>(type: "int", nullable: true),
                    FollowingAmount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Followee_User_CurrentUserId",
                        column: x => x.CurrentUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_FolloweeId",
                table: "User",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_Followee_CurrentUserId",
                table: "Followee",
                column: "CurrentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Followee_FolloweeId",
                table: "User",
                column: "FolloweeId",
                principalTable: "Followee",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Followee_FolloweeId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Followee");

            migrationBuilder.DropIndex(
                name: "IX_User_FolloweeId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FolloweeId",
                table: "User");
        }
    }
}
