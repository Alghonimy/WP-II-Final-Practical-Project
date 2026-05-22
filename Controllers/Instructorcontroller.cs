using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;
namespace UniversityApp.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AppDbContext _context;
        public InstructorController(AppDbContext context) { _context = context; }
        public async Task<IActionResult> Index(string search = "")
        {
            var q = _context.Instructors.Include(i => i.Department).Include(i => i.Course).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(i => i.Name.Contains(search) || i.Email.Contains(search));
            ViewBag.Search = search;
            return View(await q.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var i = await _context.Instructors.Include(x => x.Department).Include(x => x.Course)
                        .FirstOrDefaultAsync(x => x.Id == id);
            if (i == null) return View("NotFound");
            return View(i);
        }
        public IActionResult Create() { PopulateDropdowns(); return View(); }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Instructor created successfully!";
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(instructor.DepartmentId, instructor.CrsId);
            return View(instructor);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var inst = await _context.Instructors.FindAsync(id);
            if (inst == null) return View("NotFound");
            PopulateDropdowns(inst.DepartmentId, inst.CrsId);
            return View(inst);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor)
        {
            if (id != instructor.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                _context.Instructors.Update(instructor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Instructor updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(instructor.DepartmentId, instructor.CrsId);
            return View(instructor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var inst = await _context.Instructors.Include(i => i.Department).Include(i => i.Course)
                           .FirstOrDefaultAsync(i => i.Id == id);
            if (inst == null) return View("NotFound");
            return View(inst);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inst = await _context.Instructors.FindAsync(id);
            if (inst != null) { _context.Instructors.Remove(inst); await _context.SaveChangesAsync(); }
            TempData["Success"] = "Instructor deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(int? dept = null, int? crs = null)
        {
            ViewBag.Departments = new SelectList(_context.Departments.OrderBy(d => d.Name), "Id", "Name", dept);
            ViewBag.Courses = new SelectList(_context.Courses.OrderBy(c => c.Name), "Id", "Name", crs);
        }
    }
}