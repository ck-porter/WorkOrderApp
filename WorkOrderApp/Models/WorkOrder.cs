using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace WorkOrderApp.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Open;
        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
        public string? Location { get; set; } = null;

        public int? AssignedToId { get; set; }
        public Employee? AssignedTo { get; set; }
        public ICollection<WorkOrderLog> History { get; set; } = new List<WorkOrderLog>();
    }

    public enum WorkOrderStatus
    {
        Open,
        Assigned,
        InProgress,
        Completed, 
        Archived
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
