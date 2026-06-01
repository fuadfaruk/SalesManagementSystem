using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SalesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9c5e6baa-d8fb-4ad4-bf47-05f624a509a4", "d1f25502-da5a-4572-8653-d68a0e7c1caf", "User", "USER" },
                    { "c69d7990-ae54-4b39-b4a0-9c110d3ff19e", "088a455c-0d26-4533-9eb3-90b1275e0d27", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c5e6baa-d8fb-4ad4-bf47-05f624a509a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c69d7990-ae54-4b39-b4a0-9c110d3ff19e");
        }
    }
}
