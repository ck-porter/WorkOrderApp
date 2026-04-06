using WorkOrderApp.Models;

namespace WorkOrderApp.Services
{
    public interface IWorkOrderService
    {
        Task<List<WorkOrder>> GetAllAsync();
        Task<List<WorkOrder>> GetForTechnicianAsync(string technicianId);
        Task<WorkOrder?> GetByIdAsync(int Id);
        Task<WorkOrder> CreateAsync(WorkOrder workOrder, string userId);

        Task UpdateAsync(WorkOrder workOrder, string userId);
        Task DeleteAsync(int id);

    }
}
