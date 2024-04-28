using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRCodeAttendance.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Departments_DepartmentId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "EmployeeCount",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "TotalEmployees",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "TotalPositions",
                table: "Departments");

            migrationBuilder.AlterColumn<long>(
                name: "DepartmentId",
                table: "Positions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Positions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Departments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Departments_DepartmentId",
                table: "Positions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Departments_DepartmentId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Departments");

            migrationBuilder.AlterColumn<long>(
                name: "DepartmentId",
                table: "Positions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeCount",
                table: "Positions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalEmployees",
                table: "Departments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPositions",
                table: "Departments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Departments_DepartmentId",
                table: "Positions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
