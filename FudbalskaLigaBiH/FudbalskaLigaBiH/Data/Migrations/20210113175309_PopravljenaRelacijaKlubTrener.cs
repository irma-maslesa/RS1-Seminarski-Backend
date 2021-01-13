using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Data.Migrations
{
    public partial class PopravljenaRelacijaKlubTrener : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trener_Klub_KlubID",
                table: "Trener");

            migrationBuilder.DropIndex(
                name: "IX_Trener_KlubID",
                table: "Trener");

            migrationBuilder.DropColumn(
                name: "KlubID",
                table: "Trener");

            migrationBuilder.CreateIndex(
                name: "IX_Klub_TrenerID",
                table: "Klub",
                column: "TrenerID",
                unique: true,
                filter: "[TrenerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Klub_Trener_TrenerID",
                table: "Klub",
                column: "TrenerID",
                principalTable: "Trener",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klub_Trener_TrenerID",
                table: "Klub");

            migrationBuilder.DropIndex(
                name: "IX_Klub_TrenerID",
                table: "Klub");

            migrationBuilder.AddColumn<int>(
                name: "KlubID",
                table: "Trener",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trener_KlubID",
                table: "Trener",
                column: "KlubID",
                unique: true,
                filter: "[KlubID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Trener_Klub_KlubID",
                table: "Trener",
                column: "KlubID",
                principalTable: "Klub",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
