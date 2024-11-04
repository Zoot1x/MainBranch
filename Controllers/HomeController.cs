using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.Admin.Models.Parser;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Data.EF;
using Project.ViewModels;

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
                var currentSemester = currentUser.CurrentSemester;

                var speciality = _Parsercontext.Specialities.FirstOrDefault(s =>
                    s.Groups.Any(g => g.Number == groupNumber)
                );

                if (speciality != null)
                {
                    int course = (currentUser.GroupNumber.Value / 10) % 10;

                    var disciplines = _Parsercontext
                        .Semesters.Where(s => s.Number == currentSemester 
                            && s.Discipline.Speciality == speciality)
                        .Select(s => s.Discipline)
                        .Distinct()
                        .ToList();

                    var homeviewmodel = new HomeViewModel
                    {
                        Disciplines = disciplines,
                        GroupNumber = currentUser.GroupNumber,
                        Speciality = speciality.Title,
                        KursNumber = course,
                        SemesterNumber = currentUser.CurrentSemester,
                        Name = currentUser.Name,
                        LastName = currentUser.LastName,
                        FatherName = currentUser.FatherName,
                    };

                    return View(homeviewmodel);
                }
            }

            return View("Error"); // Возврат представления с сообщением об ошибке (если не удалось найти группу или дисциплины)
        }

        // private bool IsDisciplineInCurrentSemester(Discipline discipline, int? currentSemester)
        // {
        //     // Пример фильтрации по свойствам дисциплины
        //     if (currentSemester == null)
        //         return false;

        //     // Проверяем семестры, в которых сдается дисциплина
        //     int? examSemester = int.TryParse(discipline.Exam, out var exam) ? exam : (int?)null;
        //     int? scoreMarkSemester = int.TryParse(discipline.Score_mark, out var scoreMark) ? scoreMark : (int?)null;
        //     int? scoreNoMarkSemester = int.TryParse(discipline.Score_no_mark, out var scoreNoMark) ? scoreNoMark : (int?)null;
        //     int? courseworkSemester = int.TryParse(discipline.Coursework, out var coursework) ? coursework : (int?)null;

        //     // Проверяем, соответствует ли текущий семестр одному из семестров дисциплины
        //     return (examSemester == null || currentSemester == examSemester)
        //         && (scoreMarkSemester == null || currentSemester == scoreMarkSemester)
        //         && (scoreNoMarkSemester == null || currentSemester == scoreNoMarkSemester)
        //         && (courseworkSemester == null || currentSemester == courseworkSemester);
        // }
    }
}
