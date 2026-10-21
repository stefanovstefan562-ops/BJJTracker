using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPractitionerToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PractitionerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PractitionerId",
                table: "AspNetUsers",
                column: "PractitionerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Practitioners_PractitionerId",
                table: "AspNetUsers",
                column: "PractitionerId",
                principalTable: "Practitioners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Practitioners_PractitionerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PractitionerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PractitionerId",
                table: "AspNetUsers");
        }
    }
}
