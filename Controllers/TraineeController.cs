using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;

namespace UniversityApp.Controllers
{
    public class TraineeController : Controller
    {
        private readonly AppDbContext _context;
        public TraineeController(AppDbContext context) { _context = context; }

        public async Task<IActionResult> Index(string search = "")
        {
            var q = _context.Trainees.Include(t => t.Department).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(t => t.Name.Contains(search) || t.Grade.Contains(search));
            ViewBag.Search = search;
            return View(await q.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var t = await _context.Trainees.Include(x => x.Department)
                        .Include(x => x.CrsResults).ThenInclude(cr => cr.Course)
                        .FirstOrDefaultAsync(x => x.Id == id);
            if (t == null) return View("NotFound");
            return View(t);
        }

        public IActionResult Create() { PopulateDropdowns(); return View(); }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                _context.Trainees.Add(trainee);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Trainee created successfully!";
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(trainee.DepartmentId);
            return View(trainee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var t = await _context.Trainees.FindAsync(id);
            if (t == null) return View("NotFound");
            PopulateDropdowns(t.DepartmentId);
            return View(t);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trainee trainee)
        {
            if (id != trainee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                _context.Trainees.Update(trainee);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Trainee updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(trainee.DepartmentId);
            return View(trainee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var t = await _context.Trainees.Include(x => x.Department)
                        .FirstOrDefaultAsync(x => x.Id == id);
            if (t == null) return View("NotFound");
            return View(t);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var t = await _context.Trainees.FindAsync(id);
            if (t != null) { _context.Trainees.Remove(t); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Trainee deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(int? dept = null) =>
            ViewBag.Departments = new SelectList(_context.Departments.OrderBy(d => d.Name), "Id", "Name", dept);
    }
}