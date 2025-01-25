using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICC_Champion_Trophy_2025.Migrations
{
    /// <inheritdoc />
    public partial class PlayerTableNameUpdat1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerDetails_PlayerId",
                table: "PlayerDetails");

            migrationBuilder.RenameColumn(
                name: "HighScore",
                table: "Players_new",
                newName: "HighestScore");

            migrationBuilder.AddColumn<int>(
                name: "PlayersId",
                table: "Players_new",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_new_PlayersId",
                table: "Players_new",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDetails_PlayerId",
                table: "PlayerDetails",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_new_Players_new_PlayersId",
                table: "Players_new",
                column: "PlayersId",
                principalTable: "Players_new",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_new_Players_new_PlayersId",
                table: "Players_new");

            migrationBuilder.DropIndex(
                name: "IX_Players_new_PlayersId",
                table: "Players_new");

            migrationBuilder.DropIndex(
                name: "IX_PlayerDetails_PlayerId",
                table: "PlayerDetails");

            migrationBuilder.DropColumn(
                name: "PlayersId",
                table: "Players_new");

            migrationBuilder.RenameColumn(
                name: "HighestScore",
                table: "Players_new",
                newName: "HighScore");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDetails_PlayerId",
                table: "PlayerDetails",
                column: "PlayerId",
                unique: true);
        }
    }
}
