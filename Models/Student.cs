using System.ComponentModel;

namespace ASP.Net_labb_2_School_App.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }

        [DisplayName("Courses By Student")]
        public ICollection<CoursesStudent> CoursesStudents { get; set; } = new List<CoursesStudent>();
    }
}
