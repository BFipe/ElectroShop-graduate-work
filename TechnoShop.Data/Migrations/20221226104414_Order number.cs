using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnoShop.Data.Migrations
{
    public partial class Ordernumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "UserOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "UserOrders");
        }
    }
}
