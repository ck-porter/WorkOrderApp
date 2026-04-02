namespace WorkOrderApp.Models
{
    public class WorkOrderLog
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string Action { get; set; } = null!; 
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string PerformedBy { get; set; } = null!; // Employee name or email who performed the action
    }
}
