using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOffer_MenuItems_MenuItemId",
                table: "MenuItemOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOffer_Offers_OfferId",
                table: "MenuItemOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemOffer",
                table: "MenuItemOffer");

            migrationBuilder.RenameTable(
                name: "MenuItemOffer",
                newName: "MenuItemOffers");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemOffer_OfferId",
                table: "MenuItemOffers",
                newName: "IX_MenuItemOffers_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemOffer_MenuItemId",
                table: "MenuItemOffers",
                newName: "IX_MenuItemOffers_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemOffers",
                table: "MenuItemOffers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOffers_MenuItems_MenuItemId",
                table: "MenuItemOffers",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOffers_Offers_OfferId",
                table: "MenuItemOffers",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOffers_MenuItems_MenuItemId",
                table: "MenuItemOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOffers_Offers_OfferId",
                table: "MenuItemOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemOffers",
                table: "MenuItemOffers");

            migrationBuilder.RenameTable(
                name: "MenuItemOffers",
                newName: "MenuItemOffer");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemOffers_OfferId",
                table: "MenuItemOffer",
                newName: "IX_MenuItemOffer_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemOffers_MenuItemId",
                table: "MenuItemOffer",
                newName: "IX_MenuItemOffer_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemOffer",
                table: "MenuItemOffer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOffer_MenuItems_MenuItemId",
                table: "MenuItemOffer",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOffer_Offers_OfferId",
                table: "MenuItemOffer",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }
    }
}
