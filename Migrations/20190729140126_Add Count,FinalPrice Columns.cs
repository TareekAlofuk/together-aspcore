using Microsoft.EntityFrameworkCore.Migrations;

namespace together_aspcore.Migrations
{
    public partial class AddCountFinalPriceColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Count",
                table: "ServicesUsages",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FinalPrice",
                table: "ServicesUsages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "ServicesUsages");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "ServicesUsages");
        }
    }
}
