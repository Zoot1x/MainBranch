using Project.Areas.Admin.Models.Parser;

namespace Project.Areas.Admin.ViewModels
{
    public class DisciplineTableViewModel 
    { 
        public List<Discipline>? Disciplines { get; set; }
        public string? SpecialityTitle {get; set;}
    }
}
