using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblConfig");

            migrationBuilder.DropTable(
                name: "TblContractPacking");

            migrationBuilder.DropTable(
                name: "TblContractProduct");

            migrationBuilder.DropTable(
                name: "TblCustomerGroup");

            migrationBuilder.DropTable(
                name: "TblLocation");

            migrationBuilder.DropTable(
                name: "TblPortageDocument");

            migrationBuilder.DropTable(
                name: "TblPortageInjury");

            migrationBuilder.DropTable(
                name: "TblPortageMoney");

            migrationBuilder.DropTable(
                name: "TblPortageRowInjury");

            migrationBuilder.DropTable(
                name: "TblStoreLog");

            migrationBuilder.DropTable(
                name: "TblUserPermis");

            migrationBuilder.DropTable(
                name: "TblUserSal");

            migrationBuilder.DropTable(
                name: "TblPacking");

            migrationBuilder.DropTable(
                name: "TblProduct");

            migrationBuilder.DropTable(
                name: "TblGroup");

            migrationBuilder.DropTable(
                name: "TblInjury");

            migrationBuilder.DropTable(
                name: "TblPortageRow");

            migrationBuilder.DropTable(
                name: "TblUser");

            migrationBuilder.DropTable(
                name: "TblContract");

            migrationBuilder.DropTable(
                name: "TblPortage");

            migrationBuilder.DropTable(
                name: "TblCar");

            migrationBuilder.DropTable(
                name: "TblContractType");

            migrationBuilder.DropTable(
                name: "TblCustomer");

            migrationBuilder.DropTable(
                name: "TblSalMali");
        }
    }
}
