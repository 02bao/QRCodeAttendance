using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeAttendance.Migrations
{
    /// <inheritdoc />
    public partial class _8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SqlPositionId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SqlPositionId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SqlPositionId",
                table: "Users",
                column: "SqlPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_SqlPositionId",
                table: "Users",
                column: "SqlPositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_SqlPositionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SqlPositionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SqlPositionId",
                table: "Users");
        }
    }
}
