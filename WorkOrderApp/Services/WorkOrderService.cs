using Microsoft.EntityFrameworkCore;
using WorkOrderApp.Data;
using WorkOrderApp.Models;

namespace WorkOrderApp.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly WorkOrderAppContext _context;
        public WorkOrderService(WorkOrderAppContext context)
        {
            _context = context;
        }
        public async Task<List<WorkOrder>> GetAllAsync()
        {
            return await _context.WorkOrders
                .Include(w => w.AssignedToUser)
                .ToListAsync();
        }
        public async Task<List<WorkOrder>> GetForTechnicianAsync(string technicianId)
        {
            return await _context.WorkOrders
              .Where(w => w.AssignedToUserId == technicianId)
              .Include(w => w.AssignedToUser)
              .ToListAsync();
        }
        public async Task<WorkOrder?> GetByIdAsync(int id)
        {
            return await _context.WorkOrders
            .Include(w => w.AssignedToUser)
            .Include(w => w.Logs)
                .ThenInclude(l => l.PerformedByUser)
            .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WorkOrder> CreateAsync(WorkOrder workOrder, string userId)
        {
            workOrder.CreatedAt = DateTime.UtcNow;
            _context.WorkOrders.Add(workOrder);
            await _context.SaveChangesAsync();

            _context.WorkOrderLogs.Add(new WorkOrderLog
            {
                WorkOrderId = workOrder.Id,
                Action = "Created",
                PerformedByUserId = userId,
            });

            if (!string.IsNullOrEmpty(workOrder.AssignedToUserId)) 
            {
                _context.WorkOrderLogs.Add(new WorkOrderLog
                {
                    WorkOrderId = workOrder.Id,
                    Action = "Assigned",
                    AssignedToUserId = workOrder.AssignedToUserId,
                    PerformedByUserId = userId
                });
            }

            await _context.SaveChangesAsync();
            return workOrder;
        }

        public async Task UpdateAsync(WorkOrder workOrder, string userId)
        {
            var existing = await _context.WorkOrders
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == workOrder.Id);

            if (existing == null)
                return;

            _context.WorkOrders.Update(workOrder);
            await _context.SaveChangesAsync();

            if (existing.AssignedToUserId != workOrder.AssignedToUserId)
            {
                _context.WorkOrderLogs.Add(new WorkOrderLog
                {
                    WorkOrderId = workOrder.Id,
                    Action = "Reassigned",
                    AssignedToUserId = workOrder.AssignedToUserId,
                    PerformedByUserId = userId
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder != null) 
            {
                _context.WorkOrders.Remove(workOrder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
