namespace ASP.Net_labb_2_School_App.Models
{
    public class CoursesStudent
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId {  get; set; }  
        public Course Course { get; set; } 
    }
}
