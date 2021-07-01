using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class WightScale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblConfig");

            migrationBuilder.AddColumn<float>(
                name: "WightScale",
                table: "TblProduct",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WightScale",
                table: "TblPacking",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WightScale",
                table: "TblProduct");

            migrationBuilder.DropColumn(
                name: "WightScale",
                table: "TblPacking");

            migrationBuilder.CreateTable(
                name: "TblConfig",
                columns: table => new
                {
                    ApiPass = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApiUrlPaivest = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApiUser = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    degatBascul = table.Column<int>(type: "int", nullable: true),
                    kindOtherSystem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SalMaliMain = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
