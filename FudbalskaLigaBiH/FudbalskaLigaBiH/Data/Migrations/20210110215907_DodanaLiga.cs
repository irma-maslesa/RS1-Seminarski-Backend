using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Data.Migrations
{
    public partial class DodanaLiga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LigaID",
                table: "Sezona",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Sezona_LigaID",
                table: "Sezona",
                column: "LigaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sezona_Liga_LigaID",
                table: "Sezona",
                column: "LigaID",
                principalTable: "Liga",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sezona_Liga_LigaID",
                table: "Sezona");

            migrationBuilder.DropTable(
                name: "Liga");

            migrationBuilder.DropIndex(
                name: "IX_Sezona_LigaID",
                table: "Sezona");

            migrationBuilder.DropColumn(
                name: "LigaID",
                table: "Sezona");
        }
    }
}
