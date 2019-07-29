using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace together_aspcore.Migrations
{
    public partial class AddingServicesModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceMembershipDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MembershipType = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true),
                    Count = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceMembershipDefaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceMembershipDefaults_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicesRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ServiceId = table.Column<int>(nullable: false),
                    MembershipType = table.Column<int>(nullable: false),
                    LimitType = table.Column<int>(nullable: false),
                    Discount = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesRules_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesStore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ServiceId = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesStore_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesStore_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesUsages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ServiceId = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    ReferencePerson = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: true),
                    Commission = table.Column<double>(nullable: true),
                    Discount = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesUsages_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesUsages_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceMembershipDefaults_ServiceId",
                table: "ServiceMembershipDefaults",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesRules_ServiceId",
                table: "ServicesRules",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesStore_MemberId",
                table: "ServicesStore",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesStore_ServiceId",
                table: "ServicesStore",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesUsages_MemberId",
                table: "ServicesUsages",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesUsages_ServiceId",
                table: "ServicesUsages",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceMembershipDefaults");

            migrationBuilder.DropTable(
                name: "ServicesRules");

            migrationBuilder.DropTable(
                name: "ServicesStore");

            migrationBuilder.DropTable(
                name: "ServicesUsages");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
