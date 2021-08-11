using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefsKiss.Data.Migrations
{
    public partial class RenamedAuthorToWriter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Writers_AuthorId1",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AuthorId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_WriterId",
                table: "Recipes",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Writers_WriterId",
                table: "Recipes",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Writers_WriterId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_WriterId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId1",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AuthorId1",
                table: "Recipes",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Writers_AuthorId1",
                table: "Recipes",
                column: "AuthorId1",
                principalTable: "Writers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
