using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddPractitionerToSparringSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SparringResult",
                table: "SparringSessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SparringPrtnerName",
                table: "SparringSessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SparringPartnerAcademy",
                table: "SparringSessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PractitionerId",
                table: "SparringSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SparringSessions_PractitionerId",
                table: "SparringSessions",
                column: "PractitionerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SparringSessions_Practitioners_PractitionerId",
                table: "SparringSessions",
                column: "PractitionerId",
                principalTable: "Practitioners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SparringSessions_Practitioners_PractitionerId",
                table: "SparringSessions");

            migrationBuilder.DropIndex(
                name: "IX_SparringSessions_PractitionerId",
                table: "SparringSessions");

            migrationBuilder.DropColumn(
                name: "PractitionerId",
                table: "SparringSessions");

            migrationBuilder.AlterColumn<int>(
                name: "SparringResult",
                table: "SparringSessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SparringPrtnerName",
                table: "SparringSessions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "SparringPartnerAcademy",
                table: "SparringSessions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
