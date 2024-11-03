using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public string? Password { get; set; }
        public int? GroupNumber { get; set; }
        public Roles Role { get; set; }
    }

    public enum Roles
    {
        [Display(Name = "Курсант")]
        Cadet,
        [Display(Name = "Преподаватель")]
        Teacher,
        [Display(Name = "Модератор")]
        Moderator,
        [Display(Name = "Администратор")]
        Admin,
    }
}
