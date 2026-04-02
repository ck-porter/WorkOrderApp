using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkOrderApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin@kingscounty.ca", "Admin", "Password!0", "Admin" },
                    { 2, "cheryl@kingscounty.ca", "Cheryl", "Password!0", "Employee" },
                    { 3, "mike@kingscounty.ca", "Mike", "Password!0", "Employee" }
                });

            migrationBuilder.InsertData(
                table: "WorkOrders",
                columns: new[] { "Id", "AssignedToId", "CreatedAt", "Description", "Status", "Title" },
                values: new object[,]
                {
                    { 3, null, new DateTime(2026, 4, 2, 19, 14, 7, 764, DateTimeKind.Utc).AddTicks(9430), "The air vents in the conference room are dirty and need cleaning.", "Open", "Clean Air Vents" },
                    { 1, 3, new DateTime(2026, 4, 2, 19, 14, 7, 764, DateTimeKind.Utc).AddTicks(8347), "Light fixture in hallway is flickering.", "Open", "Fix broken light" },
                    { 2, 2, new DateTime(2026, 4, 2, 19, 14, 7, 764, DateTimeKind.Utc).AddTicks(9427), "Maintenance room door hinge is loose.", "Open", "Replace door hinge" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
