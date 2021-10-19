using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    brojNotifikacija = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entitet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Liga",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liga", x => x.ID);
                });

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
                name: "Trener",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    DatumRodjenja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trener", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Novost",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    DatumObjave = table.Column<DateTime>(nullable: false),
                    KorisnikId = table.Column<string>(nullable: true),
                    Slika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novost", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Novost_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    EntitetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grad_Entitet_EntitetID",
                        column: x => x.EntitetID,
                        principalTable: "Entitet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sezona",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumPocetka = table.Column<DateTime>(nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(nullable: false),
                    LigaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sezona", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sezona_Liga_LigaID",
                        column: x => x.LigaID,
                        principalTable: "Liga",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stadion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Kapacitet = table.Column<int>(nullable: false),
                    GradID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stadion_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Klub",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Slika = table.Column<string>(nullable: true),
                    TrenerID = table.Column<int>(nullable: true),
                    StadionID = table.Column<int>(nullable: false),
                    LigaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klub", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Klub_Liga_LigaID",
                        column: x => x.LigaID,
                        principalTable: "Liga",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Klub_Stadion_StadionID",
                        column: x => x.StadionID,
                        principalTable: "Stadion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Klub_Trener_TrenerID",
                        column: x => x.TrenerID,
                        principalTable: "Trener",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    KlubID = table.Column<int>(nullable: true),
                    Slika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igrac", x => x.IgracID);
                    table.ForeignKey(
                        name: "FK_Igrac_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

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
                    IsPoluvrijeme = table.Column<bool>(nullable: false),
                    MinutaIgre = table.Column<int>(nullable: false),
                    LigaID = table.Column<int>(nullable: false),
                    SezonaID = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Utakmica_Liga_LigaID",
                        column: x => x.LigaID,
                        principalTable: "Liga",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Utakmica_Sezona_SezonaID",
                        column: x => x.SezonaID,
                        principalTable: "Sezona",
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

            migrationBuilder.CreateTable(
                name: "StatistikaIgrac",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Golovi = table.Column<int>(nullable: false),
                    ZutiKarton = table.Column<int>(nullable: false),
                    CrveniKarton = table.Column<int>(nullable: false),
                    BrojMinuta = table.Column<int>(nullable: false),
                    Asistencije = table.Column<int>(nullable: false),
                    IgracId = table.Column<int>(nullable: false),
                    UtakmicaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatistikaIgrac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StatistikaIgrac_Igrac_IgracId",
                        column: x => x.IgracId,
                        principalTable: "Igrac",
                        principalColumn: "IgracID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatistikaIgrac_Utakmica_UtakmicaID",
                        column: x => x.UtakmicaID,
                        principalTable: "Utakmica",
                        principalColumn: "UtakmicaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatistikaKlub",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Posjed = table.Column<int>(nullable: false),
                    Sutevi = table.Column<int>(nullable: false),
                    SuteviOkvir = table.Column<int>(nullable: false),
                    SuteviVanOkvira = table.Column<int>(nullable: false),
                    SuteviBlokirani = table.Column<int>(nullable: false),
                    Korneri = table.Column<int>(nullable: false),
                    Ofsajdi = table.Column<int>(nullable: false),
                    Odbrane = table.Column<int>(nullable: false),
                    Prekrsaji = table.Column<int>(nullable: false),
                    Dodavanja = table.Column<int>(nullable: false),
                    Uklizavanja = table.Column<int>(nullable: false),
                    ZutiKarton = table.Column<int>(nullable: false),
                    Napadi = table.Column<int>(nullable: false),
                    NapadiOpasni = table.Column<int>(nullable: false),
                    KlubId = table.Column<int>(nullable: false),
                    UtakmicaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatistikaKlub", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StatistikaKlub_Klub_KlubId",
                        column: x => x.KlubId,
                        principalTable: "Klub",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatistikaKlub_Utakmica_UtakmicaID",
                        column: x => x.UtakmicaID,
                        principalTable: "Utakmica",
                        principalColumn: "UtakmicaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Grad_EntitetID",
                table: "Grad",
                column: "EntitetID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Klub_LigaID",
                table: "Klub",
                column: "LigaID");

            migrationBuilder.CreateIndex(
                name: "IX_Klub_StadionID",
                table: "Klub",
                column: "StadionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klub_TrenerID",
                table: "Klub",
                column: "TrenerID",
                unique: true,
                filter: "[TrenerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUtakmica_KorisnikId",
                table: "KorisnikUtakmica",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUtakmica_UtakmicaID",
                table: "KorisnikUtakmica",
                column: "UtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Novost_KorisnikId",
                table: "Novost",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Sezona_LigaID",
                table: "Sezona",
                column: "LigaID");

            migrationBuilder.CreateIndex(
                name: "IX_Stadion_GradID",
                table: "Stadion",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_StatistikaIgrac_IgracId",
                table: "StatistikaIgrac",
                column: "IgracId");

            migrationBuilder.CreateIndex(
                name: "IX_StatistikaIgrac_UtakmicaID",
                table: "StatistikaIgrac",
                column: "UtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_StatistikaKlub_KlubId",
                table: "StatistikaKlub",
                column: "KlubId");

            migrationBuilder.CreateIndex(
                name: "IX_StatistikaKlub_UtakmicaID",
                table: "StatistikaKlub",
                column: "UtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_KlubDomacinID",
                table: "Utakmica",
                column: "KlubDomacinID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_KlubGostID",
                table: "Utakmica",
                column: "KlubGostID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_LigaID",
                table: "Utakmica",
                column: "LigaID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_SezonaID",
                table: "Utakmica",
                column: "SezonaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "KorisnikUtakmica");

            migrationBuilder.DropTable(
                name: "Novost");

            migrationBuilder.DropTable(
                name: "StatistikaIgrac");

            migrationBuilder.DropTable(
                name: "StatistikaKlub");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Igrac");

            migrationBuilder.DropTable(
                name: "Utakmica");

            migrationBuilder.DropTable(
                name: "Pozicija");

            migrationBuilder.DropTable(
                name: "Klub");

            migrationBuilder.DropTable(
                name: "Sezona");

            migrationBuilder.DropTable(
                name: "Stadion");

            migrationBuilder.DropTable(
                name: "Trener");

            migrationBuilder.DropTable(
                name: "Liga");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "Entitet");
        }
    }
}
