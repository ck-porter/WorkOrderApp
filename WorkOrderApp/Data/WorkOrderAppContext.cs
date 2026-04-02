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
                    Email = "admin@kingscounty.ca",
                    Password = "Password!0",
                    Role = "Admin"
                },
                 new Models.Employee
                 {
                     Id = 2,
                     Name = "Cheryl",
                     Email = "cheryl@kingscounty.ca",
                     Password = "Password!0",
                     Role = "Employee"
                 },
                  new Models.Employee
                  {
                      Id = 3,
                      Name = "Mike",
                      Email = "mike@kingscounty.ca",
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
                    Status = "Open",
                    AssignedToId = 3
                },
                new Models.WorkOrder
                {
                    Id = 2,
                    Title = "Replace door hinge",
                    Description = "Maintenance room door hinge is loose.",
                    Status = "Open",
                    AssignedToId = 2
                },
                new Models.WorkOrder
                {
                    Id = 3,
                    Title = "Clean Air Vents",
                    Description = "The air vents in the conference room are dirty and need cleaning.",
                    Status = "Open",
                    AssignedToId = null // Unassigned work order
                }
            );
        }
    }
}
