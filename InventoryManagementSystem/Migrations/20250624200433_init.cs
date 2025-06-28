 using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_Product_ProductId",
                table: "InventoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_WareHouse_WareHouseId",
                table: "InventoryItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryItem",
                table: "InventoryItem");

            migrationBuilder.RenameTable(
                name: "InventoryItem",
                newName: "InventoryItems");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItem_WareHouseId",
                table: "InventoryItems",
                newName: "IX_InventoryItems_WareHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItem_ProductId",
                table: "InventoryItems",
                newName: "IX_InventoryItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryItems",
                table: "InventoryItems",
                column: "InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Product_ProductId",
                table: "InventoryItems",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_WareHouse_WareHouseId",
                table: "InventoryItems",
                column: "WareHouseId",
                principalTable: "WareHouse",
                principalColumn: "WareHouseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Product_ProductId",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_WareHouse_WareHouseId",
                table: "InventoryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryItems",
                table: "InventoryItems");

            migrationBuilder.RenameTable(
                name: "InventoryItems",
                newName: "InventoryItem");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItems_WareHouseId",
                table: "InventoryItem",
                newName: "IX_InventoryItem_WareHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItems_ProductId",
                table: "InventoryItem",
                newName: "IX_InventoryItem_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryItem",
                table: "InventoryItem",
                column: "InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_Product_ProductId",
                table: "InventoryItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_WareHouse_WareHouseId",
                table: "InventoryItem",
                column: "WareHouseId",
                principalTable: "WareHouse",
                principalColumn: "WareHouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
