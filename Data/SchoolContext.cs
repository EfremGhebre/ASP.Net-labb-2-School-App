using ASP.Net_labb_2_School_App.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.Net_labb_2_School_App.Data
{
    public class SchoolContext: DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) 
        {

        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursesTeacher> CoursesTeachers { get; set; }
        public DbSet<CoursesStudent> CoursesStudents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CoursesStudent>()
                .HasKey(cs => new { cs.StudentId, cs.CourseId });

            modelBuilder.Entity<CoursesTeacher>()
                .HasKey(ct => new { ct.TeacherId, ct.CourseId });

            modelBuilder.Entity<CoursesStudent>()
               .HasOne(sc => sc.Student)
               .WithMany(s => s.CoursesStudents)
               .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<CoursesStudent>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.CoursesStudents)
                .HasForeignKey(sc => sc.CourseId);
        }
        
    }
}
