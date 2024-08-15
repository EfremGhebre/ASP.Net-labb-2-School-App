using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.Net_labb_2_School_App.Data;
using ASP.Net_labb_2_School_App.Models;

namespace ASP.Net_labb_2_School_App.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var studentCourses = _context.Courses.Include(sc => sc.CoursesStudents);
            return View(await studentCourses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
     
        public async Task<IActionResult> FindAllStudentsWithAllTeachers()
        {
            ViewBag.CourseId = new SelectList(await _context.Courses.ToListAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FindAllStudentsWithAllTeachers(int courseId)
        {
            var students = await _context.Courses
                .Include(cs => cs.CoursesStudents)
                .ThenInclude(s => s.Student)
                .Include(ct => ct.CoursesTeachers)
                .ThenInclude(t => t.Teacher)
                .Where(ci => ci.Id == courseId)
                .Select(i => i.CoursesStudents)
                .ToListAsync();

            ViewBag.CourseId = new SelectList(await _context.Courses.ToListAsync(), "Id", "Name", courseId);
            return View(students);
        }
        public async Task<IActionResult> FilterStudentTeacherByCourse(int? courseId)
        {
            // Populate the dropdown list with courses
            ViewBag.CourseId = new SelectList(await _context.Courses.ToListAsync(), "Id", "Name");

            // If no course is selected, return an empty view
            if (courseId == null)
            {
                return View();
            }

            // Get the course along with associated teachers and students
            var course = await _context.Courses
                .Include(c => c.CoursesStudents)
                    .ThenInclude(cs => cs.Student)
                .Include(c => c.CoursesTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound();
            }

            var model = new CourseStudentsTeachersViewModel
            {
                Course = course,
                Students = course.CoursesStudents.Select(cs => cs.Student).ToList(), 
                Teachers = course.CoursesTeachers.Select(ct => ct.Teacher).ToList()
            };

            return View(model);
        }   
    }
}

