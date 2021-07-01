using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class contracttypenavhsanad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtSanadPortageKind",
                table: "TblContractType",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtSanadPortageKind",
                table: "TblContractType");
        }
    }
}
