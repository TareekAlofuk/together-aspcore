using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace together_aspcore.Migrations
{
    public partial class makeExpirationDatenullableonServiceUsagetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesUsages_Members_MemberId",
                table: "ServicesUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesUsages_Services_ServiceId",
                table: "ServicesUsages");

            migrationBuilder.DropIndex(
                name: "IX_ServicesUsages_MemberId",
                table: "ServicesUsages");

            migrationBuilder.DropIndex(
                name: "IX_ServicesUsages_ServiceId",
                table: "ServicesUsages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "ServicesUsages",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "ServicesUsages",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesUsages_MemberId",
                table: "ServicesUsages",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesUsages_ServiceId",
                table: "ServicesUsages",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesUsages_Members_MemberId",
                table: "ServicesUsages",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesUsages_Services_ServiceId",
                table: "ServicesUsages",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
