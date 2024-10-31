

namespace Project.Models.Parser
{
    public class Semester
    {
        public int? Id {get; set;}

        public int? Number {get; set;}
        public int? StudyHours {get; set;}
        public int? SelfStudyHours {get; set;}

        //Сущность дисциплины

        public int? DisciplineId {get; set;}
        public virtual Discipline? Discipline {get; set;}
    }
}