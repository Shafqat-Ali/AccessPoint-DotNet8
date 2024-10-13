using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessPoint.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LoginHistoryChangesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstLogin",
                table: "LoginHistory");

            migrationBuilder.RenameColumn(
                name: "LoginTime",
                table: "LoginHistory",
                newName: "LoginDateTime");

            migrationBuilder.RenameColumn(
                name: "LoginId",
                table: "LoginHistory",
                newName: "LoginHistoryId");

            migrationBuilder.AlterColumn<string>(
                name: "IPAddress",
                table: "LoginHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Device",
                table: "LoginHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "LoginHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Browser",
                table: "LoginHistory");

            migrationBuilder.RenameColumn(
                name: "LoginDateTime",
                table: "LoginHistory",
                newName: "LoginTime");

            migrationBuilder.RenameColumn(
                name: "LoginHistoryId",
                table: "LoginHistory",
                newName: "LoginId");

            migrationBuilder.AlterColumn<string>(
                name: "IPAddress",
                table: "LoginHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Device",
                table: "LoginHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstLogin",
                table: "LoginHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
