namespace Project.Areas.Admin.Models.Parser
{
    public class Speciality
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? KursNumber { get; set; }
        public ICollection<Discipline>? Disciplines { get; set; }
        public ICollection<Group>? Groups { get; set; }
    }
}
