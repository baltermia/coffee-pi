using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeePi.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CoffeeRoutines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ButtonType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeOfDay = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    DaysOfWeek = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExecutionId = table.Column<int>(type: "int", nullable: true),
                    WeeklyRoutine_TimeOfDay = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeRoutines", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExecutedRoutines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoutineId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Success = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DailyRoutineId = table.Column<int>(type: "int", nullable: true),
                    WeeklyRoutineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutedRoutines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutedRoutines_CoffeeRoutines_DailyRoutineId",
                        column: x => x.DailyRoutineId,
                        principalTable: "CoffeeRoutines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExecutedRoutines_CoffeeRoutines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "CoffeeRoutines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExecutedRoutines_CoffeeRoutines_WeeklyRoutineId",
                        column: x => x.WeeklyRoutineId,
                        principalTable: "CoffeeRoutines",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeRoutines_ExecutionId",
                table: "CoffeeRoutines",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutedRoutines_DailyRoutineId",
                table: "ExecutedRoutines",
                column: "DailyRoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutedRoutines_RoutineId",
                table: "ExecutedRoutines",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutedRoutines_WeeklyRoutineId",
                table: "ExecutedRoutines",
                column: "WeeklyRoutineId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeRoutines_ExecutedRoutines_ExecutionId",
                table: "CoffeeRoutines",
                column: "ExecutionId",
                principalTable: "ExecutedRoutines",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeRoutines_ExecutedRoutines_ExecutionId",
                table: "CoffeeRoutines");

            migrationBuilder.DropTable(
                name: "ExecutedRoutines");

            migrationBuilder.DropTable(
                name: "CoffeeRoutines");
        }
    }
}
