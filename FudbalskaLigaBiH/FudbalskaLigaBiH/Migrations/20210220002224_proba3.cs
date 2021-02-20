using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Migrations
{
    public partial class proba3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utakmica",
                columns: table => new
                {
                    UtakmicaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumOdrzavanja = table.Column<DateTime>(nullable: false),
                    RezultatDomacin = table.Column<int>(nullable: false),
                    RezultatGost = table.Column<int>(nullable: false),
                    KlubDomacinID = table.Column<int>(nullable: false),
                    KlubGostID = table.Column<int>(nullable: false),
                    IsZavrsena = table.Column<bool>(nullable: false),
                    IsProduzeci = table.Column<bool>(nullable: false),
                    MinutaIgre = table.Column<int>(nullable: false),
                    IsOmiljena = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmica", x => x.UtakmicaID);
                    table.ForeignKey(
                        name: "FK_Utakmica_Klub_KlubDomacinID",
                        column: x => x.KlubDomacinID,
                        principalTable: "Klub",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Utakmica_Klub_KlubGostID",
                        column: x => x.KlubGostID,
                        principalTable: "Klub",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikUtakmica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<string>(nullable: true),
                    UtakmicaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikUtakmica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikUtakmica_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisnikUtakmica_Utakmica_UtakmicaID",
                        column: x => x.UtakmicaID,
                        principalTable: "Utakmica",
                        principalColumn: "UtakmicaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUtakmica_KorisnikId",
                table: "KorisnikUtakmica",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUtakmica_UtakmicaID",
                table: "KorisnikUtakmica",
                column: "UtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_KlubDomacinID",
                table: "Utakmica",
                column: "KlubDomacinID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_KlubGostID",
                table: "Utakmica",
                column: "KlubGostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikUtakmica");

            migrationBuilder.DropTable(
                name: "Utakmica");
        }
    }
}
