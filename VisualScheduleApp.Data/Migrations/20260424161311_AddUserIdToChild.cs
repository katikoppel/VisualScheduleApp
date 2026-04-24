using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualScheduleApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Children",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Children_UserId",
                table: "Children",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_AspNetUsers_UserId",
                table: "Children",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_AspNetUsers_UserId",
                table: "Children");

            migrationBuilder.DropIndex(
                name: "IX_Children_UserId",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Children");
        }
    }
}
