using Project.Models;

namespace Project.ViewModels
{
    public class ProfileViewModel
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public int? GroupNumber { get; set; }
        public int? KursNumber {get; set;}
        public int? CurrentSemestr { get; set; }
        public string? Role { get; set; }
    }
}
