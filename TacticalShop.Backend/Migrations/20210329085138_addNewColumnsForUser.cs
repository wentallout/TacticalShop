using Microsoft.EntityFrameworkCore.Migrations;

namespace TacticalShop.Backend.Migrations
{
    public partial class addNewColumnsForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "ProductQuantity",
                "Products",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "UserAddress",
                "AspNetUsers",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                "BrandVm",
                table => new
                {
                    BrandId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>("nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_BrandVm", x => x.BrandId); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BrandVm");

            migrationBuilder.DropColumn(
                "ProductQuantity",
                "Products");

            migrationBuilder.DropColumn(
                "UserAddress",
                "AspNetUsers");
        }
    }
}