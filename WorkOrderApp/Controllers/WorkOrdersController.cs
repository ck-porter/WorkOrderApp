using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkOrderApp.Data;
using WorkOrderApp.Models;


namespace WorkOrderApp.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly WorkOrderAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WorkOrdersController(WorkOrderAppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //add helper for user drop down
        private async Task PopulateDropdownAsync(string? selectedUserId = null)
        {
            var tech = await _userManager.GetUsersInRoleAsync("Technician");

            ViewBag.Technicians = new SelectList(tech, "Id", "Email", selectedUserId);
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {
            //load user
            var user = await _userManager.GetUserAsync(User);
            IQueryable<WorkOrder> query = _context.WorkOrders.Include(w => w.AssignedToUser);

            if (User.IsInRole("Technician"))
            {
                var techWorkOrders = _context.WorkOrders.Where(w => w.AssignedToUserId == user.Id);
                return View(await techWorkOrders.ToListAsync());
            }

            return View(await query.ToListAsync());

        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {      

            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
                 .Include(w => w.AssignedToUser)
                .Include(w => w.Logs)
                .ThenInclude(l => l.PerformedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workOrder == null)
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
            if (ModelState.IsValid)
            {
                workOrder.CreatedAt = DateTime.UtcNow; // Set creation time
                _context.Add(workOrder);
                await _context.SaveChangesAsync();

                // get the current user ID
                var userId = _userManager.GetUserId(User) ?? "System";


                //log creation
                _context.WorkOrderLogs.Add(new WorkOrderLog
                {
                    WorkOrderId = workOrder.Id,
                    Action = "Created",
                    PerformedByUserId = userId

                });

                //log assignment if assigned
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
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdownAsync(workOrder.AssignedToUserId);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders.FindAsync(id);
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
            {
                return NotFound();
            }

            var existing = await _context.WorkOrders.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrder);
                    await _context.SaveChangesAsync();

                    // get user id
                    var userId = _userManager.GetUserId(User) ?? "System";

                    //check for assignment changes
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
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(workOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownAsync(workOrder.AssignedToUserId);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
           .FirstOrDefaultAsync(m => m.Id == id);
            if (workOrder == null)
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
            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder != null)
            {
                _context.WorkOrders.Remove(workOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrders.Any(e => e.Id == id);
        }
    }
}
