using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class jointables_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorFlags_Donor_DonorId",
                table: "DonorFlags");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorFlags_Transactions_TransactionsTransId",
                table: "DonorFlags");

            migrationBuilder.DropIndex(
                name: "IX_DonorFlags_DonorId",
                table: "DonorFlags");

            migrationBuilder.DropIndex(
                name: "IX_DonorFlags_TransactionsTransId",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "TransId",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "TransactionsTransId",
                table: "DonorFlags");

            migrationBuilder.CreateTable(
                name: "DonorDonorFlags",
                columns: table => new
                {
                    DonorFlagsFlagId = table.Column<int>(type: "int", nullable: false),
                    DonorsDonorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorDonorFlags", x => new { x.DonorFlagsFlagId, x.DonorsDonorId });
                    table.ForeignKey(
                        name: "FK_DonorDonorFlags_DonorFlags_DonorFlagsFlagId",
                        column: x => x.DonorFlagsFlagId,
                        principalTable: "DonorFlags",
                        principalColumn: "FlagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonorDonorFlags_Donor_DonorsDonorId",
                        column: x => x.DonorsDonorId,
                        principalTable: "Donor",
                        principalColumn: "DonorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonorFlagsTransactions",
                columns: table => new
                {
                    DonorFlagsFlagId = table.Column<int>(type: "int", nullable: false),
                    TransactionsTransId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorFlagsTransactions", x => new { x.DonorFlagsFlagId, x.TransactionsTransId });
                    table.ForeignKey(
                        name: "FK_DonorFlagsTransactions_DonorFlags_DonorFlagsFlagId",
                        column: x => x.DonorFlagsFlagId,
                        principalTable: "DonorFlags",
                        principalColumn: "FlagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonorFlagsTransactions_Transactions_TransactionsTransId",
                        column: x => x.TransactionsTransId,
                        principalTable: "Transactions",
                        principalColumn: "TransId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonorDonorFlags_DonorsDonorId",
                table: "DonorDonorFlags",
                column: "DonorsDonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorFlagsTransactions_TransactionsTransId",
                table: "DonorFlagsTransactions",
                column: "TransactionsTransId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonorDonorFlags");

            migrationBuilder.DropTable(
                name: "DonorFlagsTransactions");

            migrationBuilder.AddColumn<int>(
                name: "DonorId",
                table: "DonorFlags",
                type: "int",
                nullable: true);

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
                name: "IX_DonorFlags_DonorId",
                table: "DonorFlags",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorFlags_TransactionsTransId",
                table: "DonorFlags",
                column: "TransactionsTransId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorFlags_Donor_DonorId",
                table: "DonorFlags",
                column: "DonorId",
                principalTable: "Donor",
                principalColumn: "DonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorFlags_Transactions_TransactionsTransId",
                table: "DonorFlags",
                column: "TransactionsTransId",
                principalTable: "Transactions",
                principalColumn: "TransId");
        }
    }
}
