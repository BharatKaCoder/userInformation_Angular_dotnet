using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace userInformation_Angular_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class nameChnaged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewUserTable",
                table: "NewUserTable");

            migrationBuilder.RenameTable(
                name: "NewUserTable",
                newName: "UserListTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserListTable",
                table: "UserListTable",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserListTable",
                table: "UserListTable");

            migrationBuilder.RenameTable(
                name: "UserListTable",
                newName: "NewUserTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewUserTable",
                table: "NewUserTable",
                column: "Id");
        }
    }
}
