using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkOrderApp.Migrations
{
    /// <inheritdoc />
    public partial class FixCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Employees_EmployeeId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_EmployeeId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "WorkOrders");

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AssignedToId",
                table: "WorkOrders",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Employees_AssignedToId",
                table: "WorkOrders",
                column: "AssignedToId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Employees_AssignedToId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_AssignedToId",
                table: "WorkOrders");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "WorkOrders",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EmployeeId" },
                values: new object[] { new DateTime(2026, 4, 2, 19, 18, 15, 910, DateTimeKind.Utc).AddTicks(3489), null });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EmployeeId" },
                values: new object[] { new DateTime(2026, 4, 2, 19, 18, 15, 910, DateTimeKind.Utc).AddTicks(4643), null });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EmployeeId" },
                values: new object[] { new DateTime(2026, 4, 2, 19, 18, 15, 910, DateTimeKind.Utc).AddTicks(4645), null });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_EmployeeId",
                table: "WorkOrders",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Employees_EmployeeId",
                table: "WorkOrders",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
