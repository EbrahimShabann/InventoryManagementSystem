using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addManagerToWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "WareHouse",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WareHouse_ApplicationUserId",
                table: "WareHouse",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WareHouse_AspNetUsers_ApplicationUserId",
                table: "WareHouse",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WareHouse_AspNetUsers_ApplicationUserId",
                table: "WareHouse");

            migrationBuilder.DropIndex(
                name: "IX_WareHouse_ApplicationUserId",
                table: "WareHouse");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "WareHouse");
        }
    }
}
