using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Txt = table.Column<string>(type: "ntext", nullable: true),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FkuserAdd = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblNotes_TblUser_FkuserAdd",
                        column: x => x.FkuserAdd,
                        principalTable: "TblUser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblNoteDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    ToDate = table.Column<DateTime>(type: "date", nullable: false),
                    KindBesorat = table.Column<int>(type: "int", nullable: false),
                    SendSms = table.Column<bool>(type: "bit", nullable: false),
                    FkTblNote = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblNoteDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblNoteDates_TblNotes_FkTblNote",
                        column: x => x.FkTblNote,
                        principalTable: "TblNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblNoteRolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForUserroll = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ForUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkTblNote = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblNoteRolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblNoteRolls_TblNotes_FkTblNote",
                        column: x => x.FkTblNote,
                        principalTable: "TblNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblNoteRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    ForUserroll = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ForUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkTblNote = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TblNoteDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblNoteRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblNoteRows_TblNoteDates_TblNoteDateId",
                        column: x => x.TblNoteDateId,
                        principalTable: "TblNoteDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblNoteRows_TblNotes_FkTblNote",
                        column: x => x.FkTblNote,
                        principalTable: "TblNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblNoteDates_FkTblNote",
                table: "TblNoteDates",
                column: "FkTblNote");

            migrationBuilder.CreateIndex(
                name: "IX_TblNoteRolls_FkTblNote",
                table: "TblNoteRolls",
                column: "FkTblNote");

            migrationBuilder.CreateIndex(
                name: "IX_TblNoteRows_Date",
                table: "TblNoteRows",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_TblNoteRows_FkTblNote",
                table: "TblNoteRows",
                column: "FkTblNote");

            migrationBuilder.CreateIndex(
                name: "IX_TblNoteRows_ForUserId",
                table: "TblNoteRows",
                column: "ForUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblNoteRows_ForUserroll",
                table: "TblNoteRows",
                column: "ForUserroll");

            migrationBuilder.CreateIndex(
                name: "IX_TblNoteRows_TblNoteDateId",
                table: "TblNoteRows",
                column: "TblNoteDateId");

            migrationBuilder.CreateIndex(
                name: "IX_TblNotes_FkuserAdd",
                table: "TblNotes",
                column: "FkuserAdd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblNoteRolls");

            migrationBuilder.DropTable(
                name: "TblNoteRows");

            migrationBuilder.DropTable(
                name: "TblNoteDates");

            migrationBuilder.DropTable(
                name: "TblNotes");
        }
    }
}
