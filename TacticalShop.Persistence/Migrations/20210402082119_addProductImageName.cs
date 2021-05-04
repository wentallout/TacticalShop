using Microsoft.EntityFrameworkCore.Migrations;

namespace TacticalShop.Persistence.Migrations
{
    public partial class addProductImageName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImageName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImageName",
                table: "Products");
        }
    }
}
