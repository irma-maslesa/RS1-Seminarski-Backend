using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Data.Migrations
{
    public partial class PopravljenaRelacijaKlubStadion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadion_Klub_KlubID",
                table: "Stadion");

            migrationBuilder.DropIndex(
                name: "IX_Stadion_KlubID",
                table: "Stadion");

            migrationBuilder.DropColumn(
                name: "KlubID",
                table: "Stadion");

            migrationBuilder.CreateIndex(
                name: "IX_Klub_StadionID",
                table: "Klub",
                column: "StadionID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Klub_Stadion_StadionID",
                table: "Klub",
                column: "StadionID",
                principalTable: "Stadion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klub_Stadion_StadionID",
                table: "Klub");

            migrationBuilder.DropIndex(
                name: "IX_Klub_StadionID",
                table: "Klub");

            migrationBuilder.AddColumn<int>(
                name: "KlubID",
                table: "Stadion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stadion_KlubID",
                table: "Stadion",
                column: "KlubID",
                unique: true,
                filter: "[KlubID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadion_Klub_KlubID",
                table: "Stadion",
                column: "KlubID",
                principalTable: "Klub",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
