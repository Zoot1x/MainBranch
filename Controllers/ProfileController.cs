using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data.EF;
using Project.Models;
using Project.ViewModels;
using System.Linq;

namespace Project.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserDbContext _context;

        public ProfileController(UserDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }

            var profileViewModel = new ProfileViewModel
            {
                UserName = currentUser.UserName,
                FirstName = currentUser.Name,
                LastName = currentUser.LastName,
                FatherName = currentUser.FatherName,
                GroupNumber = currentUser.GroupNumber,
                KursNumber = currentUser.KusrNumber,
                CurrentSemestr = currentUser.CurrentSemester,
                Role = GetRoleName(currentUser.Role)
            };

            return View(profileViewModel);
        }

        private string GetRoleName(Roles role)
        {
            return role switch
            {
                Roles.Cadet => "Курсант",
                Roles.Teacher => "Преподаватель",
                Roles.Moderator => "Модератор",
                Roles.Admin => "Администратор",
                _ => "Неизвестная должность"
            };
        }
    }
}
