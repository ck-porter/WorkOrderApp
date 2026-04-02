using Microsoft.EntityFrameworkCore;

namespace WorkOrderApp.Data
{
    public class WorkOrderAppContext : DbContext
    {
        //constructor
        public WorkOrderAppContext(DbContextOptions<WorkOrderAppContext> options) : base(options) { }

        //instances
        DbSet<Models.Employee> Employees { get; set; }
        DbSet<Models.WorkOrder> WorkOrders { get; set; }

    }
}
