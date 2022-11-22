using Microsoft.EntityFrameworkCore.Migrations;

namespace Logistic.DAL.Migrations
{
    public partial class AddedTransportationRateForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RateId",
                table: "Transportations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_RateId",
                table: "Transportations",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OrganizationId",
                table: "Cars",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Organizations_OrganizationId",
                table: "Cars",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Rates_RateId",
                table: "Transportations",
                column: "RateId",
                principalTable: "Rates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Organizations_OrganizationId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Rates_RateId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_RateId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Cars_OrganizationId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RateId",
                table: "Transportations");
        }
    }
}
