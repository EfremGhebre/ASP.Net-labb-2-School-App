namespace ASP.Net_labb_2_School_App.Models
{
    public class EditTeacherViewModel
    {
        public int SelectedStudentId { get; set; }
        public int SelectedCourseId { get; set; }
        public int SelectedTeacherId { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }

}

