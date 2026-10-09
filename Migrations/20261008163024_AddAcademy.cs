using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Practitioners_PractitionerId",
                table: "TrainingSessions");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSessions_PractitionerId",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "PractitionerId",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "Academy",
                table: "Practitioners");

            migrationBuilder.AddColumn<int>(
                name: "AcademyId",
                table: "TrainingSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AcademyId",
                table: "Practitioners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Academies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_AcademyId",
                table: "TrainingSessions",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_Practitioners_AcademyId",
                table: "Practitioners",
                column: "AcademyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Practitioners_Academies_AcademyId",
                table: "Practitioners",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Academies_AcademyId",
                table: "TrainingSessions",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practitioners_Academies_AcademyId",
                table: "Practitioners");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_Academies_AcademyId",
                table: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "Academies");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSessions_AcademyId",
                table: "TrainingSessions");

            migrationBuilder.DropIndex(
                name: "IX_Practitioners_AcademyId",
                table: "Practitioners");

            migrationBuilder.DropColumn(
                name: "AcademyId",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "AcademyId",
                table: "Practitioners");

            migrationBuilder.AddColumn<int>(
                name: "PractitionerId",
                table: "TrainingSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Academy",
                table: "Practitioners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_PractitionerId",
                table: "TrainingSessions",
                column: "PractitionerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_Practitioners_PractitionerId",
                table: "TrainingSessions",
                column: "PractitionerId",
                principalTable: "Practitioners",
                principalColumn: "Id");
        }
    }
}
