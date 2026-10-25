using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademyToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcademyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AcademyId",
                table: "AspNetUsers",
                column: "AcademyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Academies_AcademyId",
                table: "AspNetUsers",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Academies_AcademyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AcademyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AcademyId",
                table: "AspNetUsers");
        }
    }
}
