using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class tagiratKindContractType3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsContract",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsContractRequred",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContract",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "IsContractRequred",
                table: "TblContractType");
        }
    }
}
