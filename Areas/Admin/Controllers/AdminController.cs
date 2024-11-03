using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Areas.Admin.ViewModels;
using Project.Data.EF;
using Project.Models;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserDbContext _context;

    public AdminController(UserDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.ToListAsync(); // Получаем список пользователей из базы данных
        return View(users); // Передаем список пользователей в представление
    }

    public async Task<IActionResult> EditRoles(int userId)
    {
        // Получаем пользователя по его ID
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        // Получаем список всех ролей
        var allRoles = Enum.GetNames(typeof(Roles)).ToList();

        // Создаем модель для представления
        var model = new EditRolesViewModel
        {
            UserId = userId,
            UserName = user.UserName,
            AssignedRoles = user.Role.ToString(), // Преобразуем роль в строку для отображения
            AllRoles = allRoles,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRoles(int userId, List<string> roleNames)
    {
        // Получаем пользователя по его ID
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        // Обновляем роль пользователя на основе выбранных ролей
        if (roleNames.Any())
        {
            // В данном примере просто назначаем первую выбранную роль
            user.Role = (Roles)Enum.Parse(typeof(Roles), roleNames.First());
        }
        else
        {
            // Если роли не выбраны, можно установить роль по умолчанию
            user.Role = Roles.Cadet; // или другая роль по умолчанию
        }

        await _context.SaveChangesAsync(); // Сохраняем изменения в базе данных

        return RedirectToAction("Index");
    }
}
