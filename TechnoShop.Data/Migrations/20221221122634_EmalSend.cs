using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnoShop.Data.Migrations
{
    public partial class EmalSend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailSenders",
                columns: table => new
                {
                    EmailSenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSenders", x => x.EmailSenderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailSenders");
        }
    }
}
