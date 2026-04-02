using System.ComponentModel.DataAnnotations;

namespace WorkOrderApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "Employee"; // Default role is Employee
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    }
}
