using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class updateTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WareHouse_AspNetUsers_ApplicationUserId",
                table: "WareHouse");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "WareHouse",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WareHouse_AspNetUsers_ApplicationUserId",
                table: "WareHouse",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WareHouse_AspNetUsers_ApplicationUserId",
                table: "WareHouse");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "WareHouse",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_WareHouse_AspNetUsers_ApplicationUserId",
                table: "WareHouse",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
