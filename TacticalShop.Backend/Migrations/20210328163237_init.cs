using Microsoft.EntityFrameworkCore.Migrations;

namespace TacticalShop.Backend.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Brands",
                table => new
                {
                    BrandId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Brands", x => x.BrandId); });

            migrationBuilder.CreateTable(
                "Categories",
                table => new
                {
                    CategoryId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Categories", x => x.CategoryId); });

            migrationBuilder.CreateTable(
                "Products",
                table => new
                {
                    ProductId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>("nvarchar(max)", nullable: true),
                    ProductPrice = table.Column<decimal>("decimal(18,2)", nullable: false),
                    ProductDescription = table.Column<string>("nvarchar(max)", nullable: true),
                    ProductImageName = table.Column<string>("nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>("int", nullable: false),
                    CategoryId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        "FK_Products_Brands_BrandId",
                        x => x.BrandId,
                        "Brands",
                        "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Products_Categories_CategoryId",
                        x => x.CategoryId,
                        "Categories",
                        "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Products_BrandId",
                "Products",
                "BrandId");

            migrationBuilder.CreateIndex(
                "IX_Products_CategoryId",
                "Products",
                "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Products");

            migrationBuilder.DropTable(
                "Brands");

            migrationBuilder.DropTable(
                "Categories");
        }
    }
}