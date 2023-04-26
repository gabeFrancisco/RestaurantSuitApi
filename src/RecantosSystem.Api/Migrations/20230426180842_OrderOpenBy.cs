using Microsoft.EntityFrameworkCore.Migrations;

namespace RecantosSystem.Api.Migrations
{
    public partial class OrderOpenBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderSheets");

            migrationBuilder.AddColumn<string>(
                name: "OpenBy",
                table: "OrderSheets",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenBy",
                table: "OrderSheets");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrderSheets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
