using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeAttendance.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Token_Users_userId",
                table: "Token");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_tb_role_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_role",
                table: "tb_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Token",
                table: "Token");

            migrationBuilder.RenameTable(
                name: "tb_role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Token",
                newName: "Tokens");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Tokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "refreshToken",
                table: "Tokens",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "isExpired",
                table: "Tokens",
                newName: "IsExpired");

            migrationBuilder.RenameColumn(
                name: "expiredTime",
                table: "Tokens",
                newName: "ExpiredTime");

            migrationBuilder.RenameColumn(
                name: "createTime",
                table: "Tokens",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "accessToken",
                table: "Tokens",
                newName: "AccessToken");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tokens",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Token_userId",
                table: "Tokens",
                newName: "IX_Tokens_UserId");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Tokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Tokens",
                newName: "Token");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "tb_role");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Token",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "Token",
                newName: "refreshToken");

            migrationBuilder.RenameColumn(
                name: "IsExpired",
                table: "Token",
                newName: "isExpired");

            migrationBuilder.RenameColumn(
                name: "ExpiredTime",
                table: "Token",
                newName: "expiredTime");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Token",
                newName: "createTime");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "Token",
                newName: "accessToken");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Token",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_UserId",
                table: "Token",
                newName: "IX_Token_userId");

            migrationBuilder.AlterColumn<long>(
                name: "userId",
                table: "Token",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Token",
                table: "Token",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_role",
                table: "tb_role",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Token_Users_userId",
                table: "Token",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_tb_role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "tb_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
