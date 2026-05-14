using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddingForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    branch_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branch_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mgr_id = table.Column<int>(type: "int", nullable: false),
                    mgr_start_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.branch_id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    emp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    super_id = table.Column<int>(type: "int", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.emp_id);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_branch_id",
                        column: x => x.branch_id,
                        principalTable: "Branches",
                        principalColumn: "branch_id");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_super_id",
                        column: x => x.super_id,
                        principalTable: "Employees",
                        principalColumn: "emp_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_mgr_id",
                table: "Branches",
                column: "mgr_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_branch_id",
                table: "Employees",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_super_id",
                table: "Employees",
                column: "super_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_mgr_id",
                table: "Branches",
                column: "mgr_id",
                principalTable: "Employees",
                principalColumn: "emp_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_mgr_id",
                table: "Branches");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
