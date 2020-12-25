using Microsoft.EntityFrameworkCore.Migrations;

namespace A35Mge.MySqlDatabase.Migrations
{
    public partial class changemenuField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desription",
                table: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "Desription",
                table: "Menu",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
