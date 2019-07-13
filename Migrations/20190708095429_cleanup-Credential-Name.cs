using Microsoft.EntityFrameworkCore.Migrations;

namespace together_aspcore.Migrations
{
    public partial class cleanupCredentialName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Members_MyMemberId",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "MyMemberId",
                table: "Credentials",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Credentials_MyMemberId",
                table: "Credentials",
                newName: "IX_Credentials_MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Members_MemberId",
                table: "Credentials",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Members_MemberId",
                table: "Credentials");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Credentials",
                newName: "MyMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Credentials_MemberId",
                table: "Credentials",
                newName: "IX_Credentials_MyMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Members_MyMemberId",
                table: "Credentials",
                column: "MyMemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
