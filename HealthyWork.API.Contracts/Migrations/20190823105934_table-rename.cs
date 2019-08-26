using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthyWork.API.Contracts.Migrations
{
    public partial class tablerename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_HeadQuarters_HeadQuarters",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramPushes_Rooms_Room",
                table: "TelegramPushes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_Room",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Rooms_Room",
                table: "Values");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Values",
                table: "Values");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TelegramPushes",
                table: "TelegramPushes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configurations",
                table: "Configurations");

            migrationBuilder.RenameTable(
                name: "Values",
                newName: "Value");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "TelegramPushes",
                newName: "TelegramPush");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "Configurations",
                newName: "Configuration");

            migrationBuilder.RenameIndex(
                name: "IX_Values_Room",
                table: "Value",
                newName: "IX_Value_Room");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Room",
                table: "User",
                newName: "IX_User_Room");

            migrationBuilder.RenameIndex(
                name: "IX_TelegramPushes_Room",
                table: "TelegramPush",
                newName: "IX_TelegramPush_Room");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_HeadQuarters",
                table: "Room",
                newName: "IX_Room_HeadQuarters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Value",
                table: "Value",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TelegramPush",
                table: "TelegramPush",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configuration",
                table: "Configuration",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_HeadQuarters_HeadQuarters",
                table: "Room",
                column: "HeadQuarters",
                principalTable: "HeadQuarters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramPush_Room_Room",
                table: "TelegramPush",
                column: "Room",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Room_Room",
                table: "User",
                column: "Room",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Value_Room_Room",
                table: "Value",
                column: "Room",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_HeadQuarters_HeadQuarters",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramPush_Room_Room",
                table: "TelegramPush");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Room_Room",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Value_Room_Room",
                table: "Value");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Value",
                table: "Value");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TelegramPush",
                table: "TelegramPush");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configuration",
                table: "Configuration");

            migrationBuilder.RenameTable(
                name: "Value",
                newName: "Values");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "TelegramPush",
                newName: "TelegramPushes");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "Configuration",
                newName: "Configurations");

            migrationBuilder.RenameIndex(
                name: "IX_Value_Room",
                table: "Values",
                newName: "IX_Values_Room");

            migrationBuilder.RenameIndex(
                name: "IX_User_Room",
                table: "Users",
                newName: "IX_Users_Room");

            migrationBuilder.RenameIndex(
                name: "IX_TelegramPush_Room",
                table: "TelegramPushes",
                newName: "IX_TelegramPushes_Room");

            migrationBuilder.RenameIndex(
                name: "IX_Room_HeadQuarters",
                table: "Rooms",
                newName: "IX_Rooms_HeadQuarters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Values",
                table: "Values",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TelegramPushes",
                table: "TelegramPushes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configurations",
                table: "Configurations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_HeadQuarters_HeadQuarters",
                table: "Rooms",
                column: "HeadQuarters",
                principalTable: "HeadQuarters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramPushes_Rooms_Room",
                table: "TelegramPushes",
                column: "Room",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_Room",
                table: "Users",
                column: "Room",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Rooms_Room",
                table: "Values",
                column: "Room",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
