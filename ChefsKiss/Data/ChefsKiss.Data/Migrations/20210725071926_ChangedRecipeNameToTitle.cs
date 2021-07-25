using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefsKiss.Data.Migrations
{
    public partial class ChangedRecipeNameToTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Recipes",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Recipes",
                newName: "Name");
        }
    }
}
