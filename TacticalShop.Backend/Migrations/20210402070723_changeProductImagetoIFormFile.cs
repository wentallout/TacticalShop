using Microsoft.EntityFrameworkCore.Migrations;

namespace TacticalShop.Backend.Migrations
{
    public partial class changeProductImagetoIFormFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ProductImage",
                "Products");

            migrationBuilder.AlterColumn<string>(
                "ProductName",
                "Products",
                "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "ProductName",
                "Products",
                "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                "ProductImage",
                "Products",
                "nvarchar(max)",
                nullable: true);
        }
    }
}