using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class addednewmigratin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Difficulty",
                table: "Children",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Parents",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Children");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Children",
                newName: "Difficulty");
        }
    }
}
