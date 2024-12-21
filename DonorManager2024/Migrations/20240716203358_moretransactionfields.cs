using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class moretransactionfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgramCode",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceCode",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AR",
                table: "Batches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Transactions_ClientId",
            //    table: "Transactions",
            //    column: "ClientId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Transactions_Client_ClientId",
            //    table: "Transactions",
            //    column: "ClientId",
            //    principalTable: "Client",
            //    principalColumn: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Transactions_Client_ClientId",
            //    table: "Transactions");

            //migrationBuilder.DropIndex(
            //    name: "IX_Transactions_ClientId",
            //    table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ProgramCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SourceCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AR",
                table: "Batches");
        }
    }
}
