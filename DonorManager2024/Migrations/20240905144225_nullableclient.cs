using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class nullableclient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorFlags_Client_ClientId",
                table: "DonorFlags");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "DonorFlags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorFlags_Client_ClientId",
                table: "DonorFlags",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorFlags_Client_ClientId",
                table: "DonorFlags");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "DonorFlags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DonorFlags_Client_ClientId",
                table: "DonorFlags",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
