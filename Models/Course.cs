using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.Net_labb_2_School_App.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Courses By Teacher ")]
        public ICollection<CoursesTeacher> CoursesTeachers { get; set; } = new List<CoursesTeacher>();
        [DisplayName("Courses By Student ")]
        public ICollection<CoursesStudent> CoursesStudents { get; set; } = new List<CoursesStudent>();
    }
}
 