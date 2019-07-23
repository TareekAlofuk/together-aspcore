using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace together_aspcore.Migrations
{
    public partial class FixingMemberTablejobtitlepassportnopassportexpirationdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Members",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PassportExpirationDate",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "Members",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Members",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PassportExpirationDate",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Members");
        }
    }
}
