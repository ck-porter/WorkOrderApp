using Microsoft.EntityFrameworkCore;
using WorkOrderApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace WorkOrderApp.Data
{
    public class WorkOrderAppContext : IdentityDbContext<IdentityUser>
    {
        //constructor
        public WorkOrderAppContext(DbContextOptions<WorkOrderAppContext> options) : base(options) { }

        //instances

        public DbSet<Models.WorkOrder> WorkOrders { get; set; }
        public DbSet<Models.WorkOrderLog> WorkOrderLogs { get; set; }   

        //add seed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);                      

            modelBuilder.Entity<WorkOrderLog>()
                .HasOne<WorkOrder>()
                .WithMany(w => w.History)
                .HasForeignKey(wl => wl.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
