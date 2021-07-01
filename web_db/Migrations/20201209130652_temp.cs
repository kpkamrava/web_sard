using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class temp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblTemps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    FkuserAdd = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkSalMali = table.Column<int>(type: "int", nullable: false),
                    FkuserTaiid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateTaiid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Txt = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Sign = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SignTaiid = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTemps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblTemps_TblUser_FkuserAdd",
                        column: x => x.FkuserAdd,
                        principalTable: "TblUser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblTemps_TblUser_FkuserTaiid",
                        column: x => x.FkuserTaiid,
                        principalTable: "TblUser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblTempRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fktemp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkLocation1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkLocation2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkLocation3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEdit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FkUserAdd = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkUserEdit = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MinDama = table.Column<double>(type: "float", nullable: true),
                    MaxDama = table.Column<double>(type: "float", nullable: true),
                    MotorDama = table.Column<double>(type: "float", nullable: true),
                    R = table.Column<double>(type: "float", nullable: true),
                    O = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nezafat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SamPashi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorZani = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Txt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTempRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblTempRows_TblTemps_Fktemp",
                        column: x => x.Fktemp,
                        principalTable: "TblTemps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblTempRows_Fktemp",
                table: "TblTempRows",
                column: "Fktemp");

            migrationBuilder.CreateIndex(
                name: "IX_TblTemps_FkSalMali",
                table: "TblTemps",
                column: "FkSalMali");

            migrationBuilder.CreateIndex(
                name: "IX_TblTemps_FkuserAdd",
                table: "TblTemps",
                column: "FkuserAdd");

            migrationBuilder.CreateIndex(
                name: "IX_TblTemps_FkuserTaiid",
                table: "TblTemps",
                column: "FkuserTaiid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblTempRows");

            migrationBuilder.DropTable(
                name: "TblTemps");
        }
    }
}
