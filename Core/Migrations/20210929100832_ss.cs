using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class ss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FristName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MobileNo = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileScannedItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormatedQrCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFinshed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScannedGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileScannedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileScannedItem_User",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScannedHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountOfItems = table.Column<int>(type: "int", nullable: false),
                    MailSent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MailRecive = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ScannedGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScannedHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScannedHeader_User",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScannedDetial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ScannedMasterId = table.Column<int>(type: "int", nullable: false),
                    QrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QrFormat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScannedDetial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScannedDetial_ScannedHeader",
                        column: x => x.ScannedMasterId,
                        principalTable: "ScannedHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileScannedItem_CreatedBy",
                table: "MobileScannedItem",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ScannedDetial_ScannedMasterId",
                table: "ScannedDetial",
                column: "ScannedMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ScannedHeader_CreatedBy",
                table: "ScannedHeader",
                column: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileScannedItem");

            migrationBuilder.DropTable(
                name: "ScannedDetial");

            migrationBuilder.DropTable(
                name: "ScannedHeader");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
