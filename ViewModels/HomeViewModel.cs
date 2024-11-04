using Project.Areas.Admin.Models.Parser;

namespace Project.ViewModels
{
    public class HomeViewModel
    {
        public List<Discipline>? Disciplines { get; set; }
        public int? GroupNumber {get; set;}
        public string? Speciality {get; set;}
        public int? KursNumber {get; set;}
        public int? SemesterNumber {get; set;}

        public string? Name {get; set;}
        public string? LastName {get; set;}
        public string? FatherName {get; set;}
    }
}