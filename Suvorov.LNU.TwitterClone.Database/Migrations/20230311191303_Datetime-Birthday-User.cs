using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suvorov.LNU.TwitterClone.Database.Migrations
{
    /// <inheritdoc />
    public partial class DatetimeBirthdayUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
