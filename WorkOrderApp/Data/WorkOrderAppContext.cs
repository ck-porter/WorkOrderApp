using Microsoft.EntityFrameworkCore;
using WorkOrderApp.Models;

namespace WorkOrderApp.Data
{
    public class WorkOrderAppContext : DbContext
    {
        //constructor
        public WorkOrderAppContext(DbContextOptions<WorkOrderAppContext> options) : base(options) { }

        //instances
        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.WorkOrder> WorkOrders { get; set; }
        public DbSet<Models.WorkOrderLog> WorkOrderLogs { get; set; }   

        //add seed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Employees
            modelBuilder.Entity<Models.Employee>().HasData(
                new Models.Employee
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@applevalley.gov",
                    Password = "Password!0",
                    Role = "Admin"
                },
                 new Models.Employee
                 {
                     Id = 2,
                     Name = "Cheryl Smith",
                     Email = "cheryl@applevalley.gov",
                     Password = "Password!0",
                     Role = "Employee"
                 },
                  new Models.Employee
                  {
                      Id = 3,
                      Name = "Mike Rogers",
                      Email = "mike@applevalley.gov",
                      Password = "Password!0",
                      Role = "Employee"
                  }
                  );

            //Seed data for work orders
            modelBuilder.Entity<Models.WorkOrder>().HasData(
                new Models.WorkOrder
                {
                    Id = 1,
                    Title = "Fix broken light",
                    Description = "Light fixture in hallway is flickering.",
                    Status = WorkOrderStatus.Assigned,
                    Priority = PriorityLevel.Medium,
                    Location = "Main hallway near reception desk.",
                    AssignedToId = 3,
                    CreatedAt = new DateTime(2026, 03, 27, 09, 00, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 2,
                    Title = "Replace door hinge",
                    Description = "Maintenance room door hinge is loose.",
                    Status = WorkOrderStatus.Assigned,
                    Priority = PriorityLevel.Low,
                    Location = "Maintenance room on the second floor.",
                    AssignedToId = 2,
                    CreatedAt = new DateTime(2026, 03, 27, 11, 30, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 3,
                    Title = "Clean Air Vents",
                    Description = "The air vents in the conference room are dirty and need cleaning.",
                    Status = WorkOrderStatus.Open,
                    Priority = PriorityLevel.Medium,
                    Location = "Conference room on the first floor.",
                    AssignedToId = null, // Unassigned work order
                    CreatedAt = new DateTime(2026, 03, 28, 08, 45, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 4,
                    Title = "Repair Leaking Faucet",
                    Description = "The faucet in the break room is leaking and needs to be repaired.",
                    Status = WorkOrderStatus.InProgress,
                    Priority = PriorityLevel.Medium,
                    Location = "Break room on the first floor.",
                    AssignedToId = 2,
                    CreatedAt = new DateTime(2026, 03, 28, 13, 15, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 5,
                    Title = "Inspect Fire Extinguishers",
                    Description = "Fire extinguishers throughout the building need to be inspected and serviced if necessary.",
                    Status = WorkOrderStatus.Completed,
                    Priority = PriorityLevel.High,
                    Location = "Throughout the building.",
                    AssignedToId = 3,
                    CreatedAt = new DateTime(2026, 03, 29, 10, 00, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 6,
                    Title = "Replace Carpet Tiles",
                    Description = "Several carpet tiles in the lobby are stained and need to be replaced.",
                    Status = WorkOrderStatus.Completed,
                    Priority = PriorityLevel.Low,
                    Location = "Lobby area near the entrance.",
                    AssignedToId = 2,
                    CreatedAt = new DateTime(2026, 03, 29, 14, 20, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 7,
                    Title = "Test Emergency Exit Lights",
                    Description = "Emergency exit lights need to be tested to ensure they are functioning properly.",
                    Status = WorkOrderStatus.Open,
                    Priority = PriorityLevel.High,
                    Location = "Emergency exits throughout the building.",
                    AssignedToId = null, // Unassigned work order
                    CreatedAt = new DateTime(2026, 03, 30, 09, 10, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 8,
                    Title = "Service HVAC System",
                    Description = "The HVAC system needs regular maintenance and servicing to ensure optimal performance.",
                    Status = WorkOrderStatus.Assigned,
                    Priority = PriorityLevel.High,
                    Location = "Rooftop mechanical room.",
                    AssignedToId = 3,
                    CreatedAt = new DateTime(2026, 03, 30, 15, 45, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 9,
                    Title = "Repair Elevator",
                    Description = "The elevator is making unusual noises and needs to be inspected and repaired if necessary.",
                    Status = WorkOrderStatus.InProgress,
                    Priority = PriorityLevel.High,
                    Location = "Elevator shaft near the lobby.",
                    AssignedToId = 2,
                    CreatedAt = new DateTime(2026, 03, 29, 14, 20, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 10,
                    Title = "Replace Broken Window",
                    Description = "A window in the conference room is cracked and needs to be replaced.",
                    Status = WorkOrderStatus.Archived,
                    Priority = PriorityLevel.Medium,
                    Location = "Conference room on the first floor.",
                    AssignedToId = 2,
                    CreatedAt = new DateTime(2026, 03, 30, 15, 45, 00, DateTimeKind.Utc)
                },
                new Models.WorkOrder
                {
                    Id = 11,
                    Title = "Inspect Roof for Leaks",
                    Description = "The roof needs to be inspected for any potential leaks or damage, especially after recent storms.",
                    Status = WorkOrderStatus.Archived,
                    Priority = PriorityLevel.High,
                    Location = "Rooftop area.",
                    AssignedToId = 3,
                    CreatedAt = new DateTime(2026, 03, 29, 14, 20, 00, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<WorkOrderLog>()
                .HasOne<WorkOrder>()
                .WithMany(w => w.History)
                .HasForeignKey(wl => wl.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
