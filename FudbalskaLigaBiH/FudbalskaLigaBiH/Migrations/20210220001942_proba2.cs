using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Migrations
{
    public partial class proba2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utakmica");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utakmica",
                columns: table => new
                {
                    UtakmicaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumOdrzavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOmiljena = table.Column<bool>(type: "bit", nullable: false),
                    IsProduzeci = table.Column<bool>(type: "bit", nullable: false),
                    IsZavrsena = table.Column<bool>(type: "bit", nullable: false),
                    MinutaIgre = table.Column<int>(type: "int", nullable: false),
                    RezultatDomacin = table.Column<int>(type: "int", nullable: false),
                    RezultatGost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmica", x => x.UtakmicaID);
                });
        }
    }
}
