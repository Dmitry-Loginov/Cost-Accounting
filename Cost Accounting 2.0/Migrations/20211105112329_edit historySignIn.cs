using Microsoft.EntityFrameworkCore.Migrations;

namespace Cost_Accounting_2._0.Migrations
{
    public partial class edithistorySignIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorySigns_AspNetUsers_UserId",
                table: "HistorySigns");

            migrationBuilder.DropIndex(
                name: "IX_HistorySigns_UserId",
                table: "HistorySigns");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "HistorySigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "HistorySigns",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorySigns_UserId1",
                table: "HistorySigns",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorySigns_AspNetUsers_UserId1",
                table: "HistorySigns",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorySigns_AspNetUsers_UserId1",
                table: "HistorySigns");

            migrationBuilder.DropIndex(
                name: "IX_HistorySigns_UserId1",
                table: "HistorySigns");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "HistorySigns");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HistorySigns",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_HistorySigns_UserId",
                table: "HistorySigns",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorySigns_AspNetUsers_UserId",
                table: "HistorySigns",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
