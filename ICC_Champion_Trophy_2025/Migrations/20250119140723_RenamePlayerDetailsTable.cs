using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICC_Champion_Trophy_2025.Migrations
{
    /// <inheritdoc />
    public partial class RenamePlayerDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaterDetails_Players_PlayerId",
                table: "PlaterDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaterDetails",
                table: "PlaterDetails");

            migrationBuilder.RenameTable(
                name: "PlaterDetails",
                newName: "PlayerDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PlaterDetails_PlayerId",
                table: "PlayerDetails",
                newName: "IX_PlayerDetails_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerDetails",
                table: "PlayerDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerDetails_Players_PlayerId",
                table: "PlayerDetails",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerDetails_Players_PlayerId",
                table: "PlayerDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerDetails",
                table: "PlayerDetails");

            migrationBuilder.RenameTable(
                name: "PlayerDetails",
                newName: "PlayerDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerDetails_PlayerId",
                table: "PlaterDetails",
                newName: "IX_PlaterDetails_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaterDetails",
                table: "PlaterDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaterDetails_Players_PlayerId",
                table: "PlaterDetails",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
