using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefsKiss.Data.Migrations
{
    public partial class RenamedIsAuthorizedToIsApproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAuthorized",
                table: "Writers",
                newName: "IsApproved");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "Writers",
                newName: "IsAuthorized");
        }
    }
}
