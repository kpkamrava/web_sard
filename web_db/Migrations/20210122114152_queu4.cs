using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class queu4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TxtReq",
                table: "TblQueus",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TxtReq",
                table: "TblQueus");
        }
    }
}
