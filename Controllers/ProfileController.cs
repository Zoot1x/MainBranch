using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager; // Specify your user class here

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index(string searchString)
        {
            var users = _userManager.Users.ToList(); // Получаем всех пользователей

            // Фильтруем пользователей по строке поиска, если она есть
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString)).ToList();
            }

            bool isAdmin = User.IsInRole("Admin");

            ViewBag.isAdmin = isAdmin;

            return View(users);
        }

    }
}
