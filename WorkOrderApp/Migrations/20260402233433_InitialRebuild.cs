using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkOrderApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialRebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedToId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Employees_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderLogs_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin@applevalley.gov", "Admin", "Password!0", "Admin" },
                    { 2, "cheryl@applevalley.gov", "Cheryl Smith", "Password!0", "Employee" },
                    { 3, "mike@applevalley.gov", "Mike Rogers", "Password!0", "Employee" }
                });

            migrationBuilder.InsertData(
                table: "WorkOrders",
                columns: new[] { "Id", "AssignedToId", "CreatedAt", "Description", "Location", "Priority", "Status", "Title" },
                values: new object[,]
                {
                    { 3, null, new DateTime(2026, 3, 28, 8, 45, 0, 0, DateTimeKind.Utc), "The air vents in the conference room are dirty and need cleaning.", "Conference room on the first floor.", 1, 0, "Clean Air Vents" },
                    { 7, null, new DateTime(2026, 3, 30, 9, 10, 0, 0, DateTimeKind.Utc), "Emergency exit lights need to be tested to ensure they are functioning properly.", "Emergency exits throughout the building.", 2, 0, "Test Emergency Exit Lights" },
                    { 1, 3, new DateTime(2026, 3, 27, 9, 0, 0, 0, DateTimeKind.Utc), "Light fixture in hallway is flickering.", "Main hallway near reception desk.", 1, 1, "Fix broken light" },
                    { 2, 2, new DateTime(2026, 3, 27, 11, 30, 0, 0, DateTimeKind.Utc), "Maintenance room door hinge is loose.", "Maintenance room on the second floor.", 0, 1, "Replace door hinge" },
                    { 4, 2, new DateTime(2026, 3, 28, 13, 15, 0, 0, DateTimeKind.Utc), "The faucet in the break room is leaking and needs to be repaired.", "Break room on the first floor.", 1, 2, "Repair Leaking Faucet" },
                    { 5, 3, new DateTime(2026, 3, 29, 10, 0, 0, 0, DateTimeKind.Utc), "Fire extinguishers throughout the building need to be inspected and serviced if necessary.", "Throughout the building.", 2, 3, "Inspect Fire Extinguishers" },
                    { 6, 2, new DateTime(2026, 3, 29, 14, 20, 0, 0, DateTimeKind.Utc), "Several carpet tiles in the lobby are stained and need to be replaced.", "Lobby area near the entrance.", 0, 3, "Replace Carpet Tiles" },
                    { 8, 3, new DateTime(2026, 3, 30, 15, 45, 0, 0, DateTimeKind.Utc), "The HVAC system needs regular maintenance and servicing to ensure optimal performance.", "Rooftop mechanical room.", 2, 1, "Service HVAC System" },
                    { 9, 2, new DateTime(2026, 3, 29, 14, 20, 0, 0, DateTimeKind.Utc), "The elevator is making unusual noises and needs to be inspected and repaired if necessary.", "Elevator shaft near the lobby.", 2, 2, "Repair Elevator" },
                    { 10, 2, new DateTime(2026, 3, 30, 15, 45, 0, 0, DateTimeKind.Utc), "A window in the conference room is cracked and needs to be replaced.", "Conference room on the first floor.", 1, 4, "Replace Broken Window" },
                    { 11, 3, new DateTime(2026, 3, 29, 14, 20, 0, 0, DateTimeKind.Utc), "The roof needs to be inspected for any potential leaks or damage, especially after recent storms.", "Rooftop area.", 2, 4, "Inspect Roof for Leaks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderLogs_WorkOrderId",
                table: "WorkOrderLogs",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AssignedToId",
                table: "WorkOrders",
                column: "AssignedToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderLogs");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
