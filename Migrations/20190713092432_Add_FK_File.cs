using Microsoft.EntityFrameworkCore.Migrations;

namespace together_aspcore.Migrations
{
    public partial class Add_FK_File : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_MemberId",
                table: "Files",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Members_MemberId",
                table: "Files",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Members_MemberId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_MemberId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Files");
        }
    }
}
