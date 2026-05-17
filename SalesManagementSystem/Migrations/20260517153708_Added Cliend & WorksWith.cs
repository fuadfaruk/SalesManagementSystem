using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedCliendWorksWith : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_mgr_id",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branches_branch_id",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_super_id",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Branches_mgr_id",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "sex",
                table: "Employees",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "salary",
                table: "Employees",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "super_id",
                table: "Employees",
                newName: "SupervisorId");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Employees",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "branch_id",
                table: "Employees",
                newName: "BranchId");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Employees",
                newName: "BirthDay");

            migrationBuilder.RenameColumn(
                name: "emp_id",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_super_id",
                table: "Employees",
                newName: "IX_Employees_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_branch_id",
                table: "Employees",
                newName: "IX_Employees_BranchId");

            migrationBuilder.RenameColumn(
                name: "mgr_start_date",
                table: "Branches",
                newName: "ManagerStartDate");

            migrationBuilder.RenameColumn(
                name: "mgr_id",
                table: "Branches",
                newName: "ManagerId");

            migrationBuilder.RenameColumn(
                name: "branch_name",
                table: "Branches",
                newName: "BranchName");

            migrationBuilder.RenameColumn(
                name: "branch_id",
                table: "Branches",
                newName: "BranchId");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorksWiths",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TotalSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksWiths", x => new { x.EmployeeId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_WorksWiths_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorksWiths_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ManagerId",
                table: "Branches",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BranchId",
                table: "Clients",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_WorksWiths_ClientId",
                table: "WorksWiths",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_ManagerId",
                table: "Branches",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_ManagerId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "WorksWiths");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ManagerId",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "Employees",
                newName: "sex");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Employees",
                newName: "salary");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "Employees",
                newName: "super_id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employees",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Employees",
                newName: "branch_id");

            migrationBuilder.RenameColumn(
                name: "BirthDay",
                table: "Employees",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "emp_id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees",
                newName: "IX_Employees_super_id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                newName: "IX_Employees_branch_id");

            migrationBuilder.RenameColumn(
                name: "ManagerStartDate",
                table: "Branches",
                newName: "mgr_start_date");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Branches",
                newName: "mgr_id");

            migrationBuilder.RenameColumn(
                name: "BranchName",
                table: "Branches",
                newName: "branch_name");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Branches",
                newName: "branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_mgr_id",
                table: "Branches",
                column: "mgr_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_mgr_id",
                table: "Branches",
                column: "mgr_id",
                principalTable: "Employees",
                principalColumn: "emp_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branches_branch_id",
                table: "Employees",
                column: "branch_id",
                principalTable: "Branches",
                principalColumn: "branch_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_super_id",
                table: "Employees",
                column: "super_id",
                principalTable: "Employees",
                principalColumn: "emp_id");
        }
    }
}
