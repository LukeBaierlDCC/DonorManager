using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class donorflagsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorFlags_Donor_DonorId",
                table: "DonorFlags");

            migrationBuilder.AlterColumn<int>(
                name: "DonorId",
                table: "DonorFlags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "DonorFlags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FlagName",
                table: "DonorFlags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "DonorFlags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_DonorFlags_ClientId",
                table: "DonorFlags",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorFlags_Client_ClientId",
                table: "DonorFlags",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonorFlags_Donor_DonorId",
                table: "DonorFlags",
                column: "DonorId",
                principalTable: "Donor",
                principalColumn: "DonorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorFlags_Client_ClientId",
                table: "DonorFlags");

            migrationBuilder.DropForeignKey(
                name: "FK_DonorFlags_Donor_DonorId",
                table: "DonorFlags");

            migrationBuilder.DropIndex(
                name: "IX_DonorFlags_ClientId",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "FlagName",
                table: "DonorFlags");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "DonorFlags");

            migrationBuilder.AlterColumn<int>(
                name: "DonorId",
                table: "DonorFlags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DonorFlags_Donor_DonorId",
                table: "DonorFlags",
                column: "DonorId",
                principalTable: "Donor",
                principalColumn: "DonorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
