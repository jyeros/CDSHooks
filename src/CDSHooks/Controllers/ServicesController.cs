using CDSHooks.Data.DBContexts;
using CDSHooks.Models;
using CDSHooks.Models.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CDSHooks.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Services.Include(c => c.Hook);
            return View((await applicationDbContext.ToListAsync()).Select(x => x.ToViewModel()).ToList());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cdsService = await _context.Services
                .Include(c => c.Hook)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cdsService == null)
            {
                return NotFound();
            }

            return View(cdsService.ToViewModel());
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewBag.HookId = new SelectList(_context.Hooks, "Id", "Id");
            return View("CreateEdit");
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HookId,Title,Description,Id,Prefetch,CodeType,Code")] ServiceViewModel serviceViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceViewModel.ToEntity());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.HookId = new SelectList(_context.Hooks, "Id", "Id", serviceViewModel.HookId);
            return View("CreateEdit", serviceViewModel);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cdsService = await _context.Services.FindAsync(id);
            if (cdsService == null)
            {
                return NotFound();
            }
            ViewBag.HookId = new SelectList(_context.Hooks, "Id", "Id", cdsService.HookId);
            ViewBag.isEdit = true;
            return View("CreateEdit", cdsService.ToViewModel());
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("HookId,Title,Description,Id,Prefetch,CodeType,Code")] ServiceViewModel serviceViewModel)
        {
            if (id != serviceViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceViewModel.ToEntity());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CDSServiceExists(serviceViewModel.Id))
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
            ViewBag.HookId = new SelectList(_context.Hooks, "Id", "Id", serviceViewModel.HookId);
            ViewBag.isEdit = true;
            return View("CreateEdit", serviceViewModel);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cdsService = await _context.Services
                .Include(c => c.Hook)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cdsService == null)
            {
                return NotFound();
            }

            return View(cdsService.ToViewModel());
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cdsService = await _context.Services.FindAsync(id);
            _context.Services.Remove(cdsService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CDSServiceExists(string id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
