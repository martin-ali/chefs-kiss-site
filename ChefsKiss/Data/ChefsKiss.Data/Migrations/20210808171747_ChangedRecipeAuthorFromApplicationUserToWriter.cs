using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefsKiss.Data.Migrations
{
    public partial class ChangedRecipeAuthorFromApplicationUserToWriter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AuthorId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Writers_WriterId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "WriterId",
                table: "Recipes",
                newName: "AuthorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_WriterId",
                table: "Recipes",
                newName: "IX_Recipes_AuthorId1");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Writers_AuthorId1",
                table: "Recipes",
                column: "AuthorId1",
                principalTable: "Writers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Writers_AuthorId1",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "AuthorId1",
                table: "Recipes",
                newName: "WriterId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_AuthorId1",
                table: "Recipes",
                newName: "IX_Recipes_WriterId");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AuthorId",
                table: "Recipes",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Writers_WriterId",
                table: "Recipes",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
