using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICC_Champion_Trophy_2025.Migrations
{
    /// <inheritdoc />
    public partial class WicktcoloumAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_new_Players_new_PlayersId",
                table: "Players_new");

            migrationBuilder.DropIndex(
                name: "IX_Players_new_PlayersId",
                table: "Players_new");

            migrationBuilder.DropColumn(
                name: "PlayersId",
                table: "Players_new");

            migrationBuilder.AddColumn<int>(
                name: "Wickets",
                table: "Players_new",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wickets",
                table: "Players_new");

            migrationBuilder.AddColumn<int>(
                name: "PlayersId",
                table: "Players_new",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_new_PlayersId",
                table: "Players_new",
                column: "PlayersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_new_Players_new_PlayersId",
                table: "Players_new",
                column: "PlayersId",
                principalTable: "Players_new",
                principalColumn: "Id");
        }
    }
}
