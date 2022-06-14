using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeePi.Data.Migrations
{
    public partial class CoffeeEnableProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "CoffeeRoutines",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "CoffeeRoutines");
        }
    }
}
