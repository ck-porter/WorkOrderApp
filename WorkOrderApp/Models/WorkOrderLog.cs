using Microsoft.AspNetCore.Identity;

namespace WorkOrderApp.Models
{
    public class WorkOrderLog
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder? WorkOrder { get; set; } // Navigation property to the related work order
        public string Action { get; set; } = null!; 
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string PerformedBy { get; set; } = null!; 
        public string? AssignedToUserId { get; set; } // Optional, only for assignment actions
        public IdentityUser? AssignedToUser { get; set; } // Navigation property for assigned user
    }
}
