using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Migrations
{
    public partial class slika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Novost",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Novost");
        }
    }
}
