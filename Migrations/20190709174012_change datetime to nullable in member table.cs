using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace together_aspcore.Migrations
{
    public partial class changedatetimetonullableinmembertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Members",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Members",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
