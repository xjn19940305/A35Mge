using Microsoft.EntityFrameworkCore.Migrations;

namespace A35Mge.MySqlDatabase.Migrations
{
    public partial class addDesfeild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desription",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desription",
                table: "Menu");
        }
    }
}
