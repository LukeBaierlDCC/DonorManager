using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class donor_edit_flagname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "DonorFlags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TransId",
                table: "DonorFlags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionsTransId",
                table: "DonorFlags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonorFlags_TransactionsTransId",
                table: "DonorFlags",
                column: "TransactionsTransId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorFlags_Transactions_TransactionsTransId",
                table: "DonorFlags",
                column: "TransactionsTransId",
                principalTable: "Transactions",
                principalColumn: "TransId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorFlags_Transactions_TransactionsTransId",
                table: "DonorFlags");

            migrationBuilder.DropIndex(
                name: "IX_DonorFlags_TransactionsTransId",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "TransId",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "TransactionsTransId",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "BatchesBatchId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "DonorFlags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
