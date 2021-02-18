using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.User.Data.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Preference_PreferenceId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "PreferenceId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Preference_PreferenceId",
                table: "User",
                column: "PreferenceId",
                principalTable: "Preference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Preference_PreferenceId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "PreferenceId",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Preference_PreferenceId",
                table: "User",
                column: "PreferenceId",
                principalTable: "Preference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
