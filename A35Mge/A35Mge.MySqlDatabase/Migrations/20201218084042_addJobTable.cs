using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace A35Mge.MySqlDatabase.Migrations
{
    public partial class addJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    JobId = table.Column<string>(nullable: true),
                    JobName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AssemblyName = table.Column<string>(nullable: true),
                    CronExpression = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    LoopType = table.Column<int>(nullable: true),
                    Params = table.Column<string>(nullable: true),
                    StartNow = table.Column<DateTime>(nullable: true),
                    TriggerType = table.Column<int>(nullable: false),
                    JobStatu = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageType",
                columns: table => new
                {
                    LanguageTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    LanguageCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageType", x => x.LanguageTypeId);
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Translate",
                columns: table => new
                {
                    TranslateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    LanguageTypeId = table.Column<int>(nullable: false),
                    TranslateCode = table.Column<string>(nullable: true),
                    TranslateContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translate", x => x.TranslateId);
                    table.ForeignKey(
                        name: "FK_Translate_LanguageType_LanguageTypeId",
                        column: x => x.LanguageTypeId,
                        principalTable: "LanguageType",
                        principalColumn: "LanguageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translate_LanguageTypeId",
                table: "Translate",
                column: "LanguageTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSchedule");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Translate");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LanguageType");
        }
    }
}
