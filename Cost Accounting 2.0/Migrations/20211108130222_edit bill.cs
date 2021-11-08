using Microsoft.EntityFrameworkCore.Migrations;

namespace Cost_Accounting_2._0.Migrations
{
    public partial class editbill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bills_Name",
                table: "Bills");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_Name",
                table: "Bills",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bills_Name",
                table: "Bills");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_Name",
                table: "Bills",
                column: "Name",
                unique: true);
        }
    }
}
