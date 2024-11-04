using Project.Areas.Admin.Models.Parser;

namespace Project.Areas.Admin.ViewModels
{
    public class SemesterTableViewModel 
    { 
        public List<Semester>? Semesters {get; set;}
        public string? DisciplineTitle {get; set;}
    }
}
