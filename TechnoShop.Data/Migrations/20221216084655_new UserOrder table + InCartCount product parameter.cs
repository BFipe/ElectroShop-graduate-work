using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnoShop.Data.Migrations
{
    public partial class newUserOrdertableInCartCountproductparameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InOrderCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserOrders",
                columns: table => new
                {
                    UserOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FlatNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Entrance = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TechnoShopUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrders", x => x.UserOrderId);
                    table.ForeignKey(
                        name: "FK_UserOrders_AspNetUsers_TechnoShopUserId",
                        column: x => x.TechnoShopUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOrderProducts",
                columns: table => new
                {
                    UserOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrderProducts", x => new { x.ProductId, x.UserOrderId });
                    table.ForeignKey(
                        name: "FK_UserOrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrderProducts_UserOrders_UserOrderId",
                        column: x => x.UserOrderId,
                        principalTable: "UserOrders",
                        principalColumn: "UserOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderProducts_UserOrderId",
                table: "UserOrderProducts",
                column: "UserOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_TechnoShopUserId",
                table: "UserOrders",
                column: "TechnoShopUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOrderProducts");

            migrationBuilder.DropTable(
                name: "UserOrders");

            migrationBuilder.DropColumn(
                name: "InOrderCount",
                table: "Products");
        }
    }
}
