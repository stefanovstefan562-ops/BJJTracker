using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class AcademyDeleteSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practitioners_Academies_AcademyId",
                table: "Practitioners");

            migrationBuilder.AddForeignKey(
                name: "FK_Practitioners_Academies_AcademyId",
                table: "Practitioners",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practitioners_Academies_AcademyId",
                table: "Practitioners");

            migrationBuilder.AddForeignKey(
                name: "FK_Practitioners_Academies_AcademyId",
                table: "Practitioners",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "Id");
        }
    }
}
