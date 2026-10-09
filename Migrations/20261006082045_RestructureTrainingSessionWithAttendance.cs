using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class RestructureTrainingSessionWithAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Practitioners_PractitionerId",
                table: "TrainingSessions");

            migrationBuilder.AlterColumn<int>(
                name: "PractitionerId",
                table: "TrainingSessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingSessionId = table.Column<int>(type: "int", nullable: false),
                    PractitionerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Practitioners_PractitionerId",
                        column: x => x.PractitionerId,
                        principalTable: "Practitioners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_TrainingSessions_TrainingSessionId",
                        column: x => x.TrainingSessionId,
                        principalTable: "TrainingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_PractitionerId",
                table: "Attendances",
                column: "PractitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_TrainingSessionId",
                table: "Attendances",
                column: "TrainingSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Practitioners_PractitionerId",
                table: "TrainingSessions",
                column: "PractitionerId",
                principalTable: "Practitioners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Practitioners_PractitionerId",
                table: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.AlterColumn<int>(
                name: "PractitionerId",
                table: "TrainingSessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Practitioners_PractitionerId",
                table: "TrainingSessions",
                column: "PractitionerId",
                principalTable: "Practitioners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
