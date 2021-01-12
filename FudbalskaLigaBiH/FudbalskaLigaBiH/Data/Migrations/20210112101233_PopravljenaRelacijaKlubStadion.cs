using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Data.Migrations
{
    public partial class PopravljenaRelacijaKlubStadion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadion_Klub_KlubID",
                table: "Stadion");

            migrationBuilder.DropIndex(
                name: "IX_Stadion_KlubID",
                table: "Stadion");

            migrationBuilder.AlterColumn<int>(
                name: "KlubID",
                table: "Stadion",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadion_Klub_KlubID",
                table: "Stadion");

            migrationBuilder.DropIndex(
                name: "IX_Stadion_KlubID",
                table: "Stadion");

            migrationBuilder.AlterColumn<int>(
                name: "KlubID",
                table: "Stadion",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stadion_KlubID",
                table: "Stadion",
                column: "KlubID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stadion_Klub_KlubID",
                table: "Stadion",
                column: "KlubID",
                principalTable: "Klub",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
