using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;
namespace UniversityApp.Controllers
{
    public class CrsResultController : Controller
    {
        private readonly AppDbContext _context;
        public CrsResultController(AppDbContext context) { _context = context; }
        public async Task<IActionResult> Index(string search = "")
        {
            var q = _context.CrsResults.Include(cr => cr.Course).Include(cr => cr.Trainee).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(cr => cr.Trainee!.Name.Contains(search) || cr.Course!.Name.Contains(search));
            ViewBag.Search = search;
            return View(await q.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var r = await _context.CrsResults.Include(cr => cr.Course).Include(cr => cr.Trainee)
                        .FirstOrDefaultAsync(cr => cr.Id == id);
            if (r == null) return View("NotFound"); return View(r);
        }
        public IActionResult Create() { PopulateDropdowns(); return View(); }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrsResult crsResult)
        {
            if (ModelState.IsValid)
            {
                _context.CrsResults.Add(crsResult); await _context.SaveChangesAsync();
                TempData["Success"] = "Result recorded successfully!";
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(crsResult.CrsId, crsResult.TraineeId);
            return View(crsResult);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var r = await _context.CrsResults.FindAsync(id);
            if (r == null) return View("NotFound");
            PopulateDropdowns(r.CrsId, r.TraineeId); return View(r);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CrsResult crsResult)
        {
            if (id != crsResult.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                _context.CrsResults.Update(crsResult); await _context.SaveChangesAsync();
                TempData["Success"] = "Result updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(crsResult.CrsId, crsResult.TraineeId);
            return View(crsResult);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var r = await _context.CrsResults.Include(cr => cr.Course).Include(cr => cr.Trainee)
                        .FirstOrDefaultAsync(cr => cr.Id == id);
            if (r == null) return View("NotFound"); return View(r);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _context.CrsResults.FindAsync(id);
            if (r != null) { _context.CrsResults.Remove(r); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Result deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(int? crs = null, int? trainee = null)
        {
            ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name", crs);
            ViewBag.Trainees = new SelectList(_context.Trainees.OrderBy(t => t.Name), "Id", "Name", trainee);
        }
    }
}