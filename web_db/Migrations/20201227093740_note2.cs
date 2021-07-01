using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class note2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblNoteRows_TblNoteDates_TblNoteDateId",
                table: "TblNoteRows");

            migrationBuilder.DropIndex(
                name: "IX_TblNoteRows_TblNoteDateId",
                table: "TblNoteRows");

            migrationBuilder.DropColumn(
                name: "TblNoteDateId",
                table: "TblNoteRows");

            migrationBuilder.AddColumn<bool>(
                name: "SendSms",
                table: "TblNoteRows",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendSms",
                table: "TblNoteRows");

            migrationBuilder.AddColumn<Guid>(
                name: "TblNoteDateId",
                table: "TblNoteRows",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblNoteRows_TblNoteDateId",
                table: "TblNoteRows",
                column: "TblNoteDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblNoteRows_TblNoteDates_TblNoteDateId",
                table: "TblNoteRows",
                column: "TblNoteDateId",
                principalTable: "TblNoteDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
