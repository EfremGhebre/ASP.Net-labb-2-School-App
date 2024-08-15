using ASP.Net_labb_2_School_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Net_labb_2_School_App.Data
{
    public class DbInitializer
    {
        private readonly SchoolContext _context;
        private readonly ILogger _logger;
        private static readonly Random _random = new();

        public DbInitializer(SchoolContext context, ILogger<DbInitializer>logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
                if (_context.Teachers.Any())
                {
                    return;   // DB has been seeded
                }

                await CreateTeachers();
                await CreateClasses();
                await CreateStudents();
                await CreateCourses();
                await CreateCoursesTeachers();
                await CreateCoursesStudents();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
            }
        }
        private async Task CreateTeachers()
        {
            var teachers = new[]
            {
                new Teacher { Name = "Reidar Nilsen" },
                new Teacher { Name = "Aldor Besher" },
                new Teacher { Name = "Tobias Landen" },
                new Teacher { Name = "Anas Hussein" },
                new Teacher { Name = "John Doe" },
                new Teacher { Name = "Sara Clarks" }
            };

            _context.Teachers.AddRange(teachers);
            await _context.SaveChangesAsync();
        }

        private async Task CreateClasses()
        {
            var classes = new[]
            {
                new Class { Name = ".Net 23 Hudiksvall" },
                new Class { Name = ".Net 23 Örnsköldsvik" },
                new Class { Name = ".Net 23 Sundsvall" }
            };

            _context.Classes.AddRange(classes);
            await _context.SaveChangesAsync();
        }

        private async Task CreateStudents()
        {
            var students = new List<Student>
            {
                new () { Name = "Efrem Ghebre", ClassId = 1},
                new () { Name = "Daniel Johns", ClassId = 1},
                new () { Name = "John Doe", ClassId = 1 },
                new () { Name = "Jane Smith", ClassId = 1 },
                new () { Name = "Michael Johnson", ClassId = 1 },
                new () { Name = "Emily Davis", ClassId = 1 },
                new () { Name = "Daniel Wilson", ClassId = 1 },
                new () { Name = "Emma Martinez", ClassId = 1 },
                new () { Name = "James Anderson", ClassId = 1 },
                new () { Name = "Sophia Taylor", ClassId = 1 },
                new () { Name = "Benjamin Thomas", ClassId = 2 },
                new () { Name = "Olivia Lee", ClassId = 2 },
                new () { Name = "Jacob Harris", ClassId = 3 },
                new () { Name = "Ava Clark", ClassId = 3 },
                new () { Name = "William Lewis", ClassId = 3 },
                new () { Name = "Mia Walker", ClassId = 3 },
                new () { Name = "Alexander Hall", ClassId = 3 },
                new () { Name = "Isabella Young", ClassId = 3 },
                new () { Name = "Ethan King", ClassId = 3 },
                new () { Name = "Amelia Wright", ClassId = 3 },
                new () { Name = "Mason Scott", ClassId = 3 },
                new () { Name = "Charlotte Green", ClassId = 3 },
                new () { Name = "Logan Adams", ClassId = 3 },
                new () { Name = "Harper Baker", ClassId = 3 },
                new () { Name = "Lucas Gonzalez", ClassId = 3 },
                new () { Name = "Evelyn Perez", ClassId = 2 },
                new () { Name = "Liam Roberts", ClassId = 1 },
                new () { Name = "Abigail Turner", ClassId = 1 },
                new () { Name = "Noah Parker", ClassId = 1 },
                new () { Name = "Ella Campbell", ClassId = 2 },
                new () { Name = "Henry Phillips", ClassId = 2 },
                new () { Name = "Sofia Evans", ClassId = 2 },
                new () { Name = "Elijah Edwards", ClassId = 2 },
                new () { Name = "Avery Collins", ClassId = 2 },
                new () { Name = "Sebastian Stewart", ClassId = 2 },
                new () { Name = "Grace Sanchez", ClassId = 2 },
                new () { Name = "Jack Morris", ClassId = 2 },
                new () { Name = "Lily Rogers", ClassId = 2 },
                new () { Name = "Samuel Reed", ClassId = 1 },
                new () { Name = "Aria Cook", ClassId = 2 },
                new () { Name = "David Morgan", ClassId = 2 },
                new () { Name = "Chloe Bell", ClassId = 2 }
        };
            foreach (var s in students)
            {
                await _context.Students.AddAsync(s);
            }
            await _context.SaveChangesAsync();
        }
        private async Task CreateCourses()
            {
                var courses = new List<Course>
                {
                    new Course{Name="Programming 1"},
                    new Course{Name="Programming 2"},
                    new Course{Name="HTML, CSS & JavaScript"},
                    new Course{Name="DevOps"},
                    new Course{Name="Web Applications in C#, ASP.NET"},
                    new Course{Name="Project management and Agile methods"},
                    new Course{Name="OOP in C# & .Net"},
                    new Course{Name="AI components and ML in MS Azure"},
                    new Course{Name="Design Patterns and Architecture"}
                };

                foreach (var c in courses)
                {
                    await _context.Courses.AddAsync(c);
                }
                await _context.SaveChangesAsync();
        }


        private async Task CreateCoursesTeachers()
        {
            var coursesTeacher = new List<CoursesTeacher>
            {
                new() { TeacherId = 1, CourseId = 1 },
                new() { TeacherId = 1, CourseId = 2 },
                new() { TeacherId = 1, CourseId = 3 },
                new() { TeacherId = 1, CourseId = 4 },
                new() { TeacherId = 1, CourseId = 5 },
                new() { TeacherId = 2, CourseId = 6 },
                new() { TeacherId = 2, CourseId = 7 },
                new() { TeacherId = 2, CourseId = 8 },
                new() { TeacherId = 2, CourseId = 9 },
                new() { TeacherId = 3, CourseId = 1 },
                new() { TeacherId = 3, CourseId = 2 },
                new() { TeacherId = 3, CourseId = 3 },
                new() { TeacherId = 3, CourseId = 4 },
                new() { TeacherId = 4, CourseId = 5 },
                new() { TeacherId = 4, CourseId = 6 },
                new() { TeacherId = 4, CourseId = 7 },
                new() { TeacherId = 4, CourseId = 8 },
                new() { TeacherId = 5, CourseId = 9 },
                new() { TeacherId = 5, CourseId = 1 },
                new() { TeacherId = 5, CourseId = 2 },
                new() { TeacherId = 5, CourseId = 3 },
                new() { TeacherId = 6, CourseId = 4 },
                new() { TeacherId = 6, CourseId = 5 },
                new() { TeacherId = 6, CourseId = 6 },
                new() { TeacherId = 6, CourseId = 7 }
            };
            _context.CoursesTeachers.AddRange(coursesTeacher);            
            await _context.SaveChangesAsync();
        }


        private async Task CreateCoursesStudents()
        {
            var studentCourses = new List<CoursesStudent>();      

            for (int studentId = 1; studentId <= 45; studentId++)
            {
                var courses = Enumerable.Range(1, 10).ToList();
                
                int numberOfCourses = _random.Next(3, 5);
                numberOfCourses = Math.Min(numberOfCourses, courses.Count);
                for (int i = 0; i < numberOfCourses; i++)
                {
                     studentCourses.Add(new CoursesStudent
                    {
                        StudentId = studentId,
                        CourseId = courses[i]
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
