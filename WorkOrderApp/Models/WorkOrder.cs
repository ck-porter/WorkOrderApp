using System.ComponentModel.DataAnnotations;

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
        public string Status { get; set; } = "Open"; // Default status is Open
        public int? AssignedToId { get; set; }
        public Employee? AssignedTo { get; set; }
    }
}
