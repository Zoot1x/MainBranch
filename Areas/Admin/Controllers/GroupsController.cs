using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.EF;
using Project.Areas.Admin.Models.Parser;

[Authorize(Roles = "Admin")]
public class GroupsController : Controller
{
    private readonly ParserDbContext _context;

    public GroupsController(ParserDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var groups = _context.Groups.Include(g => g.Speciality).ToList();
        return View(groups);
    }

    public IActionResult Create()
    {
        // Получаем список специальностей для выпадающего списка
        ViewBag.Specialities = _context.Specialities.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Group group)
    {
        if (ModelState.IsValid)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Группа успешно создана.";
            return RedirectToAction("Create"); // Переход на страницу со списком групп
        }
        ViewBag.Specialities = _context.Specialities.ToList();
        return View(group);
    }
}
