using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace together_aspcore.Migrations
{
    public partial class addexpirationdateandtypefieldsformember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Members",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Members",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Members");
        }
    }
}
