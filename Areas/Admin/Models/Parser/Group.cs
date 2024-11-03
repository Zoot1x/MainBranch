namespace Project.Areas.Admin.Models.Parser
{
    public class Group
    {
        public int? Id { get; set; }
        public int? Number { get; set; }
        public int? CadetsNumber { get; set; }
        public int? SpecialityId { get; set; }
        public virtual Speciality? Speciality { get; set; }
    }
}
