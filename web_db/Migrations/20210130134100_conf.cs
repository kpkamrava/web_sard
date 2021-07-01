using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class conf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblConf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false),
                    KeyStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ord = table.Column<int>(type: "int", nullable: true),
                    k1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    k2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    k3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblConf", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblConf_Key",
                table: "TblConf",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblConf");
        }
    }
}
