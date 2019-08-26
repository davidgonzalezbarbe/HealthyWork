using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthyWork.API.Contracts.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Min = table.Column<int>(nullable: false),
                    Max = table.Column<int>(nullable: false),
                    PushEnabled = table.Column<bool>(nullable: false),
                    EmailEnabled = table.Column<bool>(nullable: false),
                    TelegramEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeadQuarters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadQuarters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    HeadQuarters = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_HeadQuarters_HeadQuarters",
                        column: x => x.HeadQuarters,
                        principalTable: "HeadQuarters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPushes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChatId = table.Column<long>(nullable: false),
                    Room = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPushes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramPushes_Rooms_Room",
                        column: x => x.Room,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Room = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Rooms_Room",
                        column: x => x.Room,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SensorValue = table.Column<double>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Room = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_Rooms_Room",
                        column: x => x.Room,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HeadQuarters",
                table: "Rooms",
                column: "HeadQuarters");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPushes_Room",
                table: "TelegramPushes",
                column: "Room");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Room",
                table: "Users",
                column: "Room");

            migrationBuilder.CreateIndex(
                name: "IX_Values_Room",
                table: "Values",
                column: "Room");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "TelegramPushes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "HeadQuarters");
        }
    }
}
