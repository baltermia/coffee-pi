using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeePi.Data.Migrations
{
    public partial class AddMachinePropertiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineProperties",
                columns: table => new
                {
                    Running = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BeanStatus = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    WaterLevel = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineProperties");
        }
    }
}
