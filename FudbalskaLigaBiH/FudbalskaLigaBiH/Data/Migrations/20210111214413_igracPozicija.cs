using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Data.Migrations
{
    public partial class igracPozicija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pozicija",
                columns: table => new
                {
                    PozicijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivPozicije = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pozicija", x => x.PozicijaID);
                });

            migrationBuilder.CreateTable(
                name: "Igrac",
                columns: table => new
                {
                    IgracID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Visina = table.Column<int>(nullable: false),
                    Tezina = table.Column<int>(nullable: false),
                    BrojDresa = table.Column<int>(nullable: false),
                    GradID = table.Column<int>(nullable: false),
                    PozicijaID = table.Column<int>(nullable: false),
                    KlubID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igrac", x => x.IgracID);
                    table.ForeignKey(
                        name: "FK_Igrac_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Igrac_Klub_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klub",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igrac_Pozicija_PozicijaID",
                        column: x => x.PozicijaID,
                        principalTable: "Pozicija",
                        principalColumn: "PozicijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Igrac_GradID",
                table: "Igrac",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Igrac_KlubID",
                table: "Igrac",
                column: "KlubID");

            migrationBuilder.CreateIndex(
                name: "IX_Igrac_PozicijaID",
                table: "Igrac",
                column: "PozicijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igrac");

            migrationBuilder.DropTable(
                name: "Pozicija");
        }
    }
}
