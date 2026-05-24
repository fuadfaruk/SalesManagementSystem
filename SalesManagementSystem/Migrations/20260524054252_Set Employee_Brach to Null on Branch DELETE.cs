using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class SetEmployee_BrachtoNullonBranchDELETE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_ManagerId",
                table: "Branches");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_ManagerId",
                table: "Branches",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_ManagerId",
                table: "Branches");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_ManagerId",
                table: "Branches",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
