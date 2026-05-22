using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;

namespace UniversityApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context) { _context = context; }

        public async Task<IActionResult> Index(string search = "")
        {
            var query = _context.Departments
                .Include(d => d.Instructors).Include(d => d.Trainees).Include(d => d.Courses)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(d => d.Name.Contains(search) || d.Manager.Contains(search));
            ViewBag.Search = search;
            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var dept = await _context.Departments
                .Include(d => d.Instructors).Include(d => d.Trainees).Include(d => d.Courses)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (dept == null) return View("NotFound");
            return View(dept);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Department created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return View("NotFound");
            return View(dept);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Department updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _context.Departments
                .Include(d => d.Instructors).Include(d => d.Trainees).Include(d => d.Courses)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (dept == null) return View("NotFound");
            return View(dept);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept != null) { _context.Departments.Remove(dept); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Department deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}