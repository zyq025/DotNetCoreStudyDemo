using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreTestDemo.Migrations
{
    public partial class UpdateNameAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "SYS_USER");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SYS_USER",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UserPwd",
                table: "SYS_USER",
                newName: "USER_PWD");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "SYS_USER",
                newName: "USER_NAME");

            migrationBuilder.AlterTable(
                name: "SYS_USER",
                comment: "用户表");

            migrationBuilder.AlterColumn<string>(
                name: "ID",
                table: "SYS_USER",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "USER_PWD",
                table: "SYS_USER",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "USER_NAME",
                table: "SYS_USER",
                type: "varchar(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "SYS_USER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SYS_USER",
                table: "SYS_USER",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SYS_USER",
                table: "SYS_USER");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "SYS_USER");

            migrationBuilder.RenameTable(
                name: "SYS_USER",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "USER_PWD",
                table: "Users",
                newName: "UserPwd");

            migrationBuilder.RenameColumn(
                name: "USER_NAME",
                table: "Users",
                newName: "UserName");

            migrationBuilder.AlterTable(
                name: "Users",
                oldComment: "用户表");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AlterColumn<string>(
                name: "UserPwd",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
