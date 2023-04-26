using Microsoft.EntityFrameworkCore.Migrations;

namespace RecantosSystem.Api.Migrations
{
    public partial class OrderState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "OrderSheets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "OrderSheets");
        }
    }
}
