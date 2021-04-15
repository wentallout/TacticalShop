using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TacticalShop.Backend.Migrations
{
    public partial class addRatingNewNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "StarRating",
                "Products",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                "Ratings",
                table => new
                {
                    ProductId = table.Column<int>("int", nullable: false),
                    UserId = table.Column<string>("nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: true),
                    Star = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new {x.UserId, x.ProductId});
                    table.ForeignKey(
                        "FK_Ratings_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Ratings_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Ratings_ProductId",
                "Ratings",
                "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Ratings");

            migrationBuilder.DropColumn(
                "StarRating",
                "Products");
        }
    }
}