using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleNotes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstNameLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_UserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "FName");
        }
    }
}
