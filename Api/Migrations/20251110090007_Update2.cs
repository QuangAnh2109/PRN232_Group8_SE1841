using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Centers_ManagerId",
                table: "Centers",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Centers_Users_ManagerId",
                table: "Centers",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Centers_Users_ManagerId",
                table: "Centers");

            migrationBuilder.DropIndex(
                name: "IX_Centers_ManagerId",
                table: "Centers");
        }
    }
}
