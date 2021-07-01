using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class queu3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearId",
                table: "TblQueus");

            migrationBuilder.AlterColumn<string>(
                name: "Addras",
                table: "TblQueus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContractID",
                table: "TblQueus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TblQueus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Txt",
                table: "TblQueus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblQueus_ContractID",
                table: "TblQueus",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_TblQueus_TblContract_ContractID",
                table: "TblQueus",
                column: "ContractID",
                principalTable: "TblContract",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblQueus_TblContract_ContractID",
                table: "TblQueus");

            migrationBuilder.DropIndex(
                name: "IX_TblQueus_ContractID",
                table: "TblQueus");

            migrationBuilder.DropColumn(
                name: "ContractID",
                table: "TblQueus");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TblQueus");

            migrationBuilder.DropColumn(
                name: "Txt",
                table: "TblQueus");

            migrationBuilder.AlterColumn<string>(
                name: "Addras",
                table: "TblQueus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "TblQueus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
