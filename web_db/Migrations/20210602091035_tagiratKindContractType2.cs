using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class tagiratKindContractType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsJabeJaii",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMahvate",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsJabeJaii",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "IsMahvate",
                table: "TblContractType");
        }
    }
}
