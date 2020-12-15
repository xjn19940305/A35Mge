using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace A35Mge.MySqlDatabase.Migrations
{
    public partial class AddMenuTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    keepAlive = table.Column<bool>(nullable: false),
                    Component = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    redirect = table.Column<string>(nullable: true),
                    Show = table.Column<bool>(nullable: false),
                    hideChildren = table.Column<bool>(nullable: false),
                    IsBtn = table.Column<bool>(nullable: false),
                    ParentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
