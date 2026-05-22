using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;
namespace UniversityApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context) { _context = context; }
        public async Task<IActionResult> Index(string search = "")
        {
            var q = _context.Courses.Include(c => c.Department).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) q = q.Where(c => c.Name.Contains(search));
            ViewBag.Search = search;
            return View(await q.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var c = await _context.Courses
                .Include(x => x.Department).Include(x => x.Instructors)
                .Include(x => x.CrsResults).ThenInclude(cr => cr.Trainee)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return View("NotFound");
            return View(c);
        }
        public IActionResult Create() { PopulateDropdowns(); return View(); }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course); await _context.SaveChangesAsync();
                TempData["Success"] = "Course created successfully!";
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(course.DepartmentId);
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _context.Courses.FindAsync(id);
            if (c == null) return View("NotFound");
            PopulateDropdowns(c.DepartmentId); return View(c);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                _context.Courses.Update(course); await _context.SaveChangesAsync();
                TempData["Success"] = "Course updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(course.DepartmentId); return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Courses.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return View("NotFound"); return View(c);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var c = await _context.Courses.FindAsync(id);
            if (c != null) { _context.Courses.Remove(c); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Course deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(int? dept = null) =>
            ViewBag.Departments = new SelectList(_context.Departments.OrderBy(d => d.Name), "Id", "Name", dept);
    }
}