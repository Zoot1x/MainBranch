

namespace Project.Models.Parser
{
    public class Group
    {
        public int? Id {get; set;}
        public string? Number {get; set;}
        public int? SpecialityId {get; set;}
        public virtual Speciality? Speciality {get; set;}
    }
}