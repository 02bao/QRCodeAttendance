using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeAttendance.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SqlDepartmentId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SqlDepartmentId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SqlDepartmentId",
                table: "Users",
                column: "SqlDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_SqlDepartmentId",
                table: "Users",
                column: "SqlDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_SqlDepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SqlDepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SqlDepartmentId",
                table: "Users");
        }
    }
}
