using System.ComponentModel.DataAnnotations;

namespace ASP.Net_labb_2_School_App.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
