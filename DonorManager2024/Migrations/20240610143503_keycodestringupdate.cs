using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class keycodestringupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CCRoster_Donor_DonorId",
            //    table: "CCRoster");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_CCRoster_Users_UsersUserId",
            //    table: "CCRoster");

            //migrationBuilder.DropIndex(
            //    name: "IX_CCRoster_DonorId",
            //    table: "CCRoster");

            //migrationBuilder.DropIndex(
            //    name: "IX_CCRoster_UsersUserId",
            //    table: "CCRoster");

            //migrationBuilder.DropColumn(
            //    name: "DonorId",
            //    table: "CCRoster");

            //migrationBuilder.DropColumn(
            //    name: "UsersUserId",
            //    table: "CCRoster");

            migrationBuilder.AlterColumn<string>(
                name: "KeyCode",
                table: "CCRoster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "KeyCode",
                table: "CCRoster",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            //migrationBuilder.AddColumn<int>(
            //    name: "DonorId",
            //    table: "CCRoster",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "UsersUserId",
            //    table: "CCRoster",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_CCRoster_DonorId",
            //    table: "CCRoster",
            //    column: "DonorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CCRoster_UsersUserId",
            //    table: "CCRoster",
            //    column: "UsersUserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CCRoster_Donor_DonorId",
            //    table: "CCRoster",
            //    column: "DonorId",
            //    principalTable: "Donor",
            //    principalColumn: "DonorId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CCRoster_Users_UsersUserId",
            //    table: "CCRoster",
            //    column: "UsersUserId",
            //    principalTable: "Users",
            //    principalColumn: "UserId",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
