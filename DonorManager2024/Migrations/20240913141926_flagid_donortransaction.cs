using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class flagid_donortransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedDonorFlagId",
                table: "Transactions",
                newName: "FlagId");

            migrationBuilder.RenameColumn(
                name: "SelectedDonorFlagId",
                table: "Donor",
                newName: "FlagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FlagId",
                table: "Transactions",
                newName: "SelectedDonorFlagId");

            migrationBuilder.RenameColumn(
                name: "FlagId",
                table: "Donor",
                newName: "SelectedDonorFlagId");
        }
    }
}
