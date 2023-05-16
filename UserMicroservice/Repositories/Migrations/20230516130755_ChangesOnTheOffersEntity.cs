using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ChangesOnTheOffersEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Menus_MenuId",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "Offers",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_MenuId",
                table: "Offers",
                newName: "IX_Offers_RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Restaurants_RestaurantId",
                table: "Offers",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Restaurants_RestaurantId",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Offers",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_RestaurantId",
                table: "Offers",
                newName: "IX_Offers_MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Menus_MenuId",
                table: "Offers",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
