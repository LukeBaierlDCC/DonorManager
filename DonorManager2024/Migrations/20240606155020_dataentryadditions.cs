using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonorManager2024.Migrations
{
    /// <inheritdoc />
    public partial class dataentryadditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nixies",
                columns: table => new
                {
                    NixieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prefix1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    First1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middle1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suffix1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prefix2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    First2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middle2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suffix2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Primary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nixies", x => x.NixieId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomainUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLvl = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CCRoster",
                columns: table => new
                {
                    CCRosterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    //DonorId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    //UsersUserId = table.Column<int>(type: "int", nullable: false),
                    PName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiftAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CCRoster", x => x.CCRosterId);
                    table.ForeignKey(
                        name: "FK_CCRoster_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_CCRoster_Donor_DonorId",
                    //    column: x => x.DonorId,
                    //    principalTable: "Donor",
                    //    principalColumn: "DonorId",
                    //    onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_CCRoster_Users_UsersUserId",
                    //    column: x => x.UsersUserId,
                    //    principalTable: "Users",
                    //    principalColumn: "UserId",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoMail",
                columns: table => new
                {
                    NoMailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    UsersUserId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoMail", x => x.NoMailId);
                    table.ForeignKey(
                        name: "FK_NoMail_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_NoMail_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CCRoster_ClientId",
                table: "CCRoster",
                column: "ClientId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CCRoster_DonorId",
            //    table: "CCRoster",
            //    column: "DonorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CCRoster_UsersUserId",
            //    table: "CCRoster",
            //    column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NoMail_ClientId",
                table: "NoMail",
                column: "ClientId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NoMail_UsersUserId",
            //    table: "NoMail",
            //    column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CCRoster");

            migrationBuilder.DropTable(
                name: "Nixies");

            migrationBuilder.DropTable(
                name: "NoMail");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
