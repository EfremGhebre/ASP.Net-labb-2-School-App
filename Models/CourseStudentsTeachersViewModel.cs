namespace ASP.Net_labb_2_School_App.Models
{
    public class CourseStudentsTeachersViewModel
    {
        public Course? Course { get; set; }
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
        public int SelectedStudentId { get; set; } 
        public int SelectedTeacherId { get; set; } 

    }
}

