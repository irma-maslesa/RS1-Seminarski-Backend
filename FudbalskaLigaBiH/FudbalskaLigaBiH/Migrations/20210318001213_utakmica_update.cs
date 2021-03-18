using Microsoft.EntityFrameworkCore.Migrations;

namespace FudbalskaLigaBiH.Migrations
{
    public partial class utakmica_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOmiljena",
                table: "Utakmica");

            migrationBuilder.AddColumn<bool>(
                name: "IsPoluvrijeme",
                table: "Utakmica",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPoluvrijeme",
                table: "Utakmica");

            migrationBuilder.AddColumn<bool>(
                name: "IsOmiljena",
                table: "Utakmica",
                type: "bit",
                nullable: true);
        }
    }
}
