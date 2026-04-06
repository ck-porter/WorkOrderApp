using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkOrderApp.Data;
using WorkOrderApp.Models;
using WorkOrderApp.Services;


namespace WorkOrderApp.Controllers
{
    public class WorkOrdersController : Controller
    {
        //private readonly WorkOrderAppContext _context;
        private readonly IWorkOrderService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public WorkOrdersController(IWorkOrderService service, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        private async Task PopulateDropdownAsync(string? selectedUserId = null)
        {
            var tech = await _userManager.GetUsersInRoleAsync("Technician");
            var stat = new List<string> { "Open", "Assigned", "InProgress", "Completed", "Archived" };
            var priortiy = new List<string> { "Low", "Medium", "High" };

            ViewBag.Technicians = new SelectList(tech, "Id", "Email", selectedUserId);
            ViewBag.Status = new SelectList(stat);
            ViewBag.Priority = new SelectList(priortiy);
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Technician"))
            {
                return View(await _service.GetForTechnicianAsync(user.Id));
            }

            return View(await _service.GetAllAsync());
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {      

            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _service.GetByIdAsync(id.Value);
            if(workOrder == null)
            {
                return NotFound();
               
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownAsync();
            return View();
        }

        // POST: WorkOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkOrder workOrder)
        {
            if(!ModelState.IsValid)
            {
                await PopulateDropdownAsync(workOrder.AssignedToUserId);
                return View(workOrder);
            }

            var userId = _userManager.GetUserId(User) ?? "System";
            await _service.CreateAsync(workOrder, userId);

            return RedirectToAction(nameof(Index));           
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _service.GetByIdAsync(id.Value);
            if (workOrder == null)
            {
                return NotFound();
            }
    
            await PopulateDropdownAsync(workOrder.AssignedToUserId);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkOrder workOrder)
        {
            if (id != workOrder.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateDropdownAsync(workOrder.AssignedToUserId);
                return View(workOrder);
            }

            var userId = _userManager.GetUserId(User) ?? "System";
            await _service.UpdateAsync(workOrder, userId);

            return RedirectToAction(nameof(Index));        
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workOrder = await _service.GetByIdAsync(id.Value);
            if(workOrder == null)
            {
                return NotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
