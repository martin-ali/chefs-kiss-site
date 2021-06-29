using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefsKiss.Data.Migrations
{
    public partial class WorkingOnApplicationUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_ApplicationUserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_AspNetUsers_ApplicationUserId",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Vote",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_ApplicationUserId",
                table: "Vote",
                newName: "IX_Vote_AuthorId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Recipes",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_ApplicationUserId",
                table: "Recipes",
                newName: "IX_Recipes_AuthorId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Comments",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                newName: "IX_Comments_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AuthorId",
                table: "Recipes",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_AspNetUsers_AuthorId",
                table: "Vote",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AuthorId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_AspNetUsers_AuthorId",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Vote",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_AuthorId",
                table: "Vote",
                newName: "IX_Vote_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Recipes",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes",
                newName: "IX_Recipes_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Comments",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                newName: "IX_Comments_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_ApplicationUserId",
                table: "Recipes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_AspNetUsers_ApplicationUserId",
                table: "Vote",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
