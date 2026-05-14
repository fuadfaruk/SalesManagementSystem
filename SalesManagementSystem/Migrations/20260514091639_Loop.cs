using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Loop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_mgr_id",
                table: "Branches");

            migrationBuilder.AlterColumn<int>(
                name: "mgr_id",
                table: "Branches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_mgr_id",
                table: "Branches",
                column: "mgr_id",
                principalTable: "Employees",
                principalColumn: "emp_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_mgr_id",
                table: "Branches");

            migrationBuilder.AlterColumn<int>(
                name: "mgr_id",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_mgr_id",
                table: "Branches",
                column: "mgr_id",
                principalTable: "Employees",
                principalColumn: "emp_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
