using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Data.Migrations
{
    public partial class DodanKlub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KlubID",
                table: "Trener",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KlubID",
                table: "Stadion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Klub",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trener_KlubID",
                table: "Trener",
                column: "KlubID",
                unique: true,
                filter: "[KlubID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stadion_KlubID",
                table: "Stadion",
                column: "KlubID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klub_LigaID",
                table: "Klub",
                column: "LigaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadion_Klub_KlubID",
                table: "Stadion",
                column: "KlubID",
                principalTable: "Klub",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trener_Klub_KlubID",
                table: "Trener",
                column: "KlubID",
                principalTable: "Klub",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadion_Klub_KlubID",
                table: "Stadion");

            migrationBuilder.DropForeignKey(
                name: "FK_Trener_Klub_KlubID",
                table: "Trener");

            migrationBuilder.DropTable(
                name: "Klub");

            migrationBuilder.DropIndex(
                name: "IX_Trener_KlubID",
                table: "Trener");

            migrationBuilder.DropIndex(
                name: "IX_Stadion_KlubID",
                table: "Stadion");

            migrationBuilder.DropColumn(
                name: "KlubID",
                table: "Trener");

            migrationBuilder.DropColumn(
                name: "KlubID",
                table: "Stadion");
        }
    }
}
