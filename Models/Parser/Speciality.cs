

namespace Project.Models.Parser
{
    public class Speciality
    {
        public int? Id {get; set;}
        
        public string? Title {get; set;}
        public ICollection<Discipline>? Disciplines {get; set;}
    }
}