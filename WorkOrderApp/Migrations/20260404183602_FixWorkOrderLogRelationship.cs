using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkOrderApp.Migrations
{
    /// <inheritdoc />
    public partial class FixWorkOrderLogRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderLogs_WorkOrders_WorkOrderId1",
                table: "WorkOrderLogs");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderLogs_WorkOrderId1",
                table: "WorkOrderLogs");

            migrationBuilder.DropColumn(
                name: "WorkOrderId1",
                table: "WorkOrderLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkOrderId1",
                table: "WorkOrderLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderLogs_WorkOrderId1",
                table: "WorkOrderLogs",
                column: "WorkOrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderLogs_WorkOrders_WorkOrderId1",
                table: "WorkOrderLogs",
                column: "WorkOrderId1",
                principalTable: "WorkOrders",
                principalColumn: "Id");
        }
    }
}
