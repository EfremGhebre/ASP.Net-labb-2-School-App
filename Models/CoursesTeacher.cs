namespace ASP.Net_labb_2_School_App.Models;

public class CoursesTeacher
{
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public int CourseId {  get; set; }
    public Course Course { get; set; }
}
