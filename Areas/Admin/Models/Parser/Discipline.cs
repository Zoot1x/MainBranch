namespace Project.Areas.Admin.Models.Parser
{
    public class Discipline
    {
        public int? Id { get; set; }

        public string? Title { get; set; }
        public string? Exam { get; set; }
        public string? Score_mark { get; set; }
        public string? Score_no_mark { get; set; }
        public string? Coursework { get; set; }

        //Сущность Семестров

        public ICollection<Semester>? Semesters { get; set; }

        //Сущность Специальностей

        public virtual Speciality? Speciality { get; set; }
        public int? SpecialityId { get; set; }
    }
}
