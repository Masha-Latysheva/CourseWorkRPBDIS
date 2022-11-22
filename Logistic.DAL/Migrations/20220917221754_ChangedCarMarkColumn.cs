using Microsoft.EntityFrameworkCore.Migrations;

namespace Logistic.DAL.Migrations
{
    public partial class ChangedCarMarkColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Cars",
                newName: "Mark");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mark",
                table: "Cars",
                newName: "LastName");
        }
    }
}
