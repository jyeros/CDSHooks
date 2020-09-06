using CDSHooks.Data.DBContexts;
using CDSHooks.Models;
using CDSHooks.Models.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CDSHooks.Controllers
{
    public class HooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hooks
        public async Task<IActionResult> Index()
        {
            return View((await _context.Hooks.ToListAsync()).Select(x => x.ToDisplayViewModel()).ToList());
        }

        // GET: Hooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hook = await _context.Hooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hook == null)
            {
                return NotFound();
            }

            return View(hook.ToDisplayViewModel());
        }

        // GET: Hooks/Create
        public IActionResult Create()
        {
            ViewBag.types = Constants.allowedTypes;
            return View("CreateEdit");
        }

        // POST: Hooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Workflow,Context")] HookViewModel hookViewModel)
        {
            if (ModelState.IsValid)
            {
                var hook = hookViewModel.ToEntity();
                _context.Add(hook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.types = Constants.allowedTypes;
            return View("CreateEdit", hookViewModel);
        }

        // GET: Hooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hook = await _context.Hooks.FindAsync(id);
            if (hook == null)
            {
                return NotFound();
            }

            ViewBag.isEdit = true;
            ViewBag.types = Constants.allowedTypes;
            return View("CreateEdit", hook.ToEditableViewModel());
        }

        // POST: Hooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Workflow,Context")] HookViewModel hookViewModel)
        {
            if (id != hookViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var hook = hookViewModel.ToEntity();
                try
                {
                    _context.Update(hook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HookExists(hook.Id))
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
            ViewBag.isEdit = true;
            ViewBag.types = Constants.allowedTypes;
            return View("CreateEdit", hookViewModel);
        }

        // GET: Hooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hook = await _context.Hooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hook == null)
            {
                return NotFound();
            }

            return View(hook.ToDisplayViewModel());
        }

        // POST: Hooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hook = await _context.Hooks.FindAsync(id);
            _context.Hooks.Remove(hook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HookExists(string id)
        {
            return _context.Hooks.Any(e => e.Id == id);
        }
    }
}
