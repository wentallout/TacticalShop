using Microsoft.EntityFrameworkCore.Migrations;

namespace TacticalShop.Backend.Migrations
{
    public partial class removeBrandVm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BrandVm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}