using System.ComponentModel;

namespace ASP.Net_labb_2_School_App.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Courses By Teacher ")]
        public ICollection<CoursesTeacher> CoursesTeachers { get; set; } = new List<CoursesTeacher>();
    }
}
