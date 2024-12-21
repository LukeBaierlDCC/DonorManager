using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class selecteddonorflagid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedDonorFlagId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SelectedDonorFlagId",
                table: "Donor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedDonorFlagId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SelectedDonorFlagId",
                table: "Donor");
        }
    }
}
