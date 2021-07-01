using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class queu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblQueus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    mob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codesend = table.Column<int>(type: "int", nullable: true),
                    datecodesend = table.Column<DateTime>(type: "datetime2", nullable: true),
                    codemeli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KindQueu = table.Column<int>(type: "int", nullable: false),
                    CodeMahsuls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<long>(type: "bigint", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblQueus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblQueus");
        }
    }
}
