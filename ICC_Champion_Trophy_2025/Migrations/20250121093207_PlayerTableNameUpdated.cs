using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICC_Champion_Trophy_2025.Migrations
{
    /// <inheritdoc />
    public partial class PlayerTableNameUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerDetails_Players_PlayerId",
                table: "PlayerDetails");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Players_Teams_TeamId",
            //    table: "Players");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Players",
            //    table: "Players");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Players_new");

            migrationBuilder.RenameIndex(
                name: "IX_Players_TeamId",
                table: "Players_new",
                newName: "IX_Players_new_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players_new",
                table: "Players_new",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerDetails_Players_new_PlayerId",
                table: "PlayerDetails",
                column: "PlayerId",
                principalTable: "Players_new",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_new_Teams_TeamId",
                table: "Players_new",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerDetails_Players_new_PlayerId",
                table: "PlayerDetails");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Players_new_Teams_TeamId",
            //    table: "Players_new");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Players_new",
            //    table: "Players_new");

            migrationBuilder.RenameTable(
                name: "Players_new",
                newName: "Players");

            migrationBuilder.RenameIndex(
                name: "IX_Players_new_TeamId",
                table: "Players",
                newName: "IX_Players_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerDetails_Players_PlayerId",
                table: "PlayerDetails",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
