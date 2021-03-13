using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Migrations
{
    public partial class as1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LigaID",
                table: "Utakmica",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Utakmica_LigaID",
                table: "Utakmica",
                column: "LigaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Utakmica_Liga_LigaID",
                table: "Utakmica",
                column: "LigaID",
                principalTable: "Liga",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utakmica_Liga_LigaID",
                table: "Utakmica");

            migrationBuilder.DropIndex(
                name: "IX_Utakmica_LigaID",
                table: "Utakmica");

            migrationBuilder.DropColumn(
                name: "LigaID",
                table: "Utakmica");
        }
    }
}
