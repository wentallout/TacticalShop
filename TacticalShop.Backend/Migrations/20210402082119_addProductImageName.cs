using Microsoft.EntityFrameworkCore.Migrations;

namespace TacticalShop.Backend.Migrations
{
    public partial class addProductImageName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "ProductImageName",
                "Products",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ProductImageName",
                "Products");
        }
    }
}