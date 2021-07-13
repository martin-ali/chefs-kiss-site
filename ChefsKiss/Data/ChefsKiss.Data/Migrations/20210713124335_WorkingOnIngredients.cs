using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefsKiss.Data.Migrations
{
    public partial class WorkingOnIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeasurementUnit",
                table: "RecipeIngredient",
                newName: "MeasurementUnitId");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "MeasurementUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_MeasurementUnitId",
                table: "RecipeIngredient",
                column: "MeasurementUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_MeasurementUnits_MeasurementUnitId",
                table: "RecipeIngredient",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_MeasurementUnits_MeasurementUnitId",
                table: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "MeasurementUnits");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_MeasurementUnitId",
                table: "RecipeIngredient");

            migrationBuilder.RenameColumn(
                name: "MeasurementUnitId",
                table: "RecipeIngredient",
                newName: "MeasurementUnit");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }
    }
}
