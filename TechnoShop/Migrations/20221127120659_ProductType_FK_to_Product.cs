using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnoShop.Migrations
{
    public partial class ProductType_FK_to_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "Products",
                type: "nvarchar(60)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProductTypes_TypeName",
                table: "ProductTypes",
                column: "TypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeName",
                table: "Products",
                column: "ProductTypeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeName",
                table: "Products",
                column: "ProductTypeName",
                principalTable: "ProductTypes",
                principalColumn: "TypeName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeName",
                table: "Products");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProductTypes_TypeName",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "Products");
        }
    }
}
