using Microsoft.EntityFrameworkCore.Migrations;

namespace Cost_Accounting_2._0.Migrations
{
    public partial class edithistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Transactions_TransactionId",
                table: "Histories");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "Histories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TypeObject",
                table: "Histories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Transactions_TransactionId",
                table: "Histories",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Transactions_TransactionId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "TypeObject",
                table: "Histories");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "Histories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Transactions_TransactionId",
                table: "Histories",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
