using Microsoft.EntityFrameworkCore.Migrations;

namespace web_db.Migrations
{
    public partial class tagiratKindContractType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "forProduct",
                table: "TblPacking");

            migrationBuilder.DropColumn(
                name: "IsMali",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "NeedLocation",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "Needbascule",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OTbasculkalaCode",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OTcodHesabFactor",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OTcodHesabKargar",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OTcodeMarkaz",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OTtypeFaktor",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OTtypeFaktorInBack",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OTtypeFaktorOut",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OTtypeFaktorOutBack",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OtcodHesabFactorInBack",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "OtcodHesabFactorOut",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "isEntry",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "isExit",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "isProduct1Packing0",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "outControlByContract",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "outControlByLocation",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "outControlByPercent",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "priceOfBoxInKargar",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "priceOfBoxOutKargar",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "priceOfKiloInKargar",
                table: "TblContractType");

            migrationBuilder.DropColumn(
                name: "priceOfKiloOutKargar",
                table: "TblContractType");

            migrationBuilder.RenameColumn(
                name: "OtcodHesabFactorOutBack",
                table: "TblContractType",
                newName: "ConfigJson");

            migrationBuilder.RenameColumn(
                name: "LocationLvlRequired",
                table: "TblContractType",
                newName: "KindCotractType");

            migrationBuilder.AddColumn<string>(
                name: "ForContractTypeJson",
                table: "TblPacking",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForContractTypeJson",
                table: "TblPacking");

            migrationBuilder.RenameColumn(
                name: "KindCotractType",
                table: "TblContractType",
                newName: "LocationLvlRequired");

            migrationBuilder.RenameColumn(
                name: "ConfigJson",
                table: "TblContractType",
                newName: "OtcodHesabFactorOutBack");

            migrationBuilder.AddColumn<bool>(
                name: "forProduct",
                table: "TblPacking",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMali",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NeedLocation",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Needbascule",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OTbasculkalaCode",
                table: "TblContractType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OTcodHesabFactor",
                table: "TblContractType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OTcodHesabKargar",
                table: "TblContractType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTcodeMarkaz",
                table: "TblContractType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTtypeFaktor",
                table: "TblContractType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTtypeFaktorInBack",
                table: "TblContractType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTtypeFaktorOut",
                table: "TblContractType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTtypeFaktorOutBack",
                table: "TblContractType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtcodHesabFactorInBack",
                table: "TblContractType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtcodHesabFactorOut",
                table: "TblContractType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isEntry",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isExit",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isProduct1Packing0",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "outControlByContract",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "outControlByLocation",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "outControlByPercent",
                table: "TblContractType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "priceOfBoxInKargar",
                table: "TblContractType",
                type: "decimal(22,4)",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<decimal>(
                name: "priceOfBoxOutKargar",
                table: "TblContractType",
                type: "decimal(22,4)",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<decimal>(
                name: "priceOfKiloInKargar",
                table: "TblContractType",
                type: "decimal(22,4)",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<decimal>(
                name: "priceOfKiloOutKargar",
                table: "TblContractType",
                type: "decimal(22,4)",
                nullable: true,
                defaultValueSql: "((0))");
        }
    }
}
