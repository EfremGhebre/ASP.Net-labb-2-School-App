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
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Students.Include(s => s.Class);
            return View(await schoolContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ClassId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", student.Class);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name", student.ClassId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ClassId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", student.ClassId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
        public async Task<IActionResult> FindAllStudentsWithAllTeachers()
        {
            var students = await _context.Students
                .Include(s => s.CoursesStudents)
                .ThenInclude(cs => cs.Course)
                .ThenInclude(c => c.CoursesTeachers)
                .ThenInclude(ct => ct.Teacher)
                .ToListAsync();

            return View(students);
        }
        [HttpGet]
        public IActionResult EditTeacher()
        {
            var viewModel = new EditTeacherViewModel
            {
                Students = _context.Students.ToList()
            };

            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult EditTeacher(EditTeacherViewModel viewModel)
        {
            // Get courses for the selected student
            if (viewModel.SelectedStudentId > 0)
            {
                viewModel.Courses = _context.Courses
                    .Include(c => c.CoursesStudents)
                    .Where(c => c.CoursesStudents.Any(cs => cs.StudentId == viewModel.SelectedStudentId))
                    .ToList();
            }

            if (viewModel.SelectedTeacherId > 0 && viewModel.SelectedCourseId > 0)
            {
                var courseTeacher = _context.CoursesTeachers
                    .FirstOrDefault(ct => ct.CourseId == viewModel.SelectedCourseId);

                if (courseTeacher != null)
                {
                    // Remove the existing teacher association
                    _context.CoursesTeachers.Remove(courseTeacher);
                    _context.SaveChanges();

                    // Add a new teacher association
                    var newCourseTeacher = new CoursesTeacher
                    {
                        CourseId = viewModel.SelectedCourseId,
                        TeacherId = viewModel.SelectedTeacherId
                    };

                    _context.CoursesTeachers.Add(newCourseTeacher);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(FindAllStudentsWithAllTeachers));
                }
            }

            // Re-populate students and teachers in case of error
            viewModel.Students = _context.Students.ToList();
            viewModel.Teachers = _context.Teachers.ToList();
            return View(viewModel);
        }
    }
}


