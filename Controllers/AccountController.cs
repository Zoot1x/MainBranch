using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.EF;
using Project.Models;
using Project.ViewModels;

namespace CustomIdentityApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserDbContext _context;

        public AccountController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Проверка на существование пользователя с таким именем
            if (await _context.Users.AnyAsync(u => u.UserName == model.UserName))
            {
                ModelState.AddModelError("", "Пользователь с таким именем уже существует");
                return View(model);
            }

            // Создаем нового пользователя с данными из модели
            var newUser = new User
            {
                UserName = model.UserName,
                Name = model.Name,
                LastName = model.LastName,
                FatherName = model.FatherName,
                GroupNumber = model.GroupNumber,
                Password = model.Password,
                Role = Roles.Cadet // Назначаем роль Cadet
                ,
            };

            // Добавляем пользователя в базу данных
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Создаем клеймы для аутентификации
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, newUser.UserName),
                new Claim(ClaimTypes.Role, newUser.Role.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            };

            // Вход нового пользователя
            await HttpContext.SignInAsync(
                "CookieAuth",
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return RedirectToAction("Index", "Home");
        }

        //<<-------------------------Логирование---------------------->>

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Проверяем, существует ли пользователь
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                return View(model);
            }

            // Создаем клеймы для аутентификации
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var authProperties = new AuthenticationProperties { IsPersistent = model.RememberMe };

            // Вход пользователя
            await HttpContext.SignInAsync(
                "CookieAuth", // Используйте "CookieAuth"
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            // Перенаправление на ReturnUrl или домашнюю страницу
            return !string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)
                ? Redirect(model.ReturnUrl)
                : RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }
    }
}
