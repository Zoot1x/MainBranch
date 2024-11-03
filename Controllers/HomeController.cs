using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Data.EF;

namespace Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;

        private readonly ParserDbContext _Parsercontext;
        private readonly UserDbContext _Usercontext;

        public HomeController(
            ParserDbContext parsercontext,
            UserDbContext usercontext,
            IDisciplineRepository disciplineRepository
        )
        {
            _Parsercontext = parsercontext;
            _Usercontext = usercontext;
            _disciplineRepository = disciplineRepository;
        }

        public IActionResult Index()
        {
            var currentUser = _Usercontext.Users.FirstOrDefault(u =>
                u.UserName == User.Identity.Name
            ); // Получить текущего авторизованного пользователя

            if (currentUser != null)
            {
                var groupNumber = currentUser.GroupNumber;
                var speciality = _Parsercontext.Specialities.FirstOrDefault(s =>
                    s.Groups.Any(g => g.Number == groupNumber)
                );

                if (speciality != null)
                {
                    var disciplines = _disciplineRepository
                        .GetAll()
                        .Where(d => d.Speciality == speciality)
                        .ToList();

                    // string disciplineTitle = _disciplineRepository.

                    return View(disciplines);
                }
            }

            return View("Error"); // Возврат представления с сообщением об ошибке (если не удалось найти группу или дисциплины)
        }
    }
}
