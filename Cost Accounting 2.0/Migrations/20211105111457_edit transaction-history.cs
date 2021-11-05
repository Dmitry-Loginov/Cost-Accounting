using Microsoft.EntityFrameworkCore.Migrations;

namespace Cost_Accounting_2._0.Migrations
{
    public partial class edittransactionhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Transactions_TransactionId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_TransactionId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Histories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Histories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_TransactionId",
                table: "Histories",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Transactions_TransactionId",
                table: "Histories",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
