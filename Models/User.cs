using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    public class User : IdentityUser
    {
        public string? Name {get; set;}
        public string? LastName {get; set;}
        public string? FatherName {get; set;}
        public int? GroupNumber {get; set;}
    }
}