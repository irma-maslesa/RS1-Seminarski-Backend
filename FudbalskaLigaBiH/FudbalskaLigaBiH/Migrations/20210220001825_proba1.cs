using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Migrations
{
    public partial class proba1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikUtakmica");

            //migrationBuilder.CreateTable(
            //    name: "Utakmica",
            //    columns: table => new
            //    {
            //        UtakmicaID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DatumOdrzavanja = table.Column<DateTime>(nullable: false),
            //        RezultatDomacin = table.Column<int>(nullable: false),
            //        RezultatGost = table.Column<int>(nullable: false),
            //        IsZavrsena = table.Column<bool>(nullable: false),
            //        IsProduzeci = table.Column<bool>(nullable: false),
            //        MinutaIgre = table.Column<int>(nullable: false),
            //        IsOmiljena = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Utakmica", x => x.UtakmicaID);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utakmica");

            migrationBuilder.CreateTable(
                name: "KorisnikUtakmica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikUtakmica", x => x.Id);
                });
        }
    }
}
