using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;

    public AdminController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        var users = _userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            users = users.Where(u => u.UserName.Contains(searchString));
        }

        return View(await users.ToListAsync());
    }

    public async Task<IActionResult> EditRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRoles(string userId, List<string> roleNames)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var currentRoles = await _userManager.GetRolesAsync(user);

        // Удаляем роли, которые не выбраны
        foreach (var role in currentRoles)
        {
            if (!roleNames.Contains(role))
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
        }

        // Добавляем новые роли
        foreach (var role in roleNames)
        {
            if (!currentRoles.Contains(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        return RedirectToAction("Index");
    }
}
