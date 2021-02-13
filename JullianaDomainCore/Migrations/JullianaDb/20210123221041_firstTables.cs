using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JullianaDomainCore.Migrations.JullianaDb
{
    public partial class firstTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JewelryOrders",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    JewelryType = table.Column<string>(nullable: true),
                    GemType = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JewelryOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JewelryOrders_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SavedJewelries",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    JewelryType = table.Column<string>(nullable: true),
                    GemType = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedJewelries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedJewelries_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JewelryOrders_UserId",
                table: "JewelryOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedJewelries_UserId",
                table: "SavedJewelries",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JewelryOrders");

            migrationBuilder.DropTable(
                name: "SavedJewelries");
        }
    }
}