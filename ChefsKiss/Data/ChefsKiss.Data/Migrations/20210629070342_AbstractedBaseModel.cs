using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefsKiss.Data.Migrations
{
    public partial class AbstractedBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Recipes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Ingredients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Ingredients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Comments",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Comments");
        }
    }
}
