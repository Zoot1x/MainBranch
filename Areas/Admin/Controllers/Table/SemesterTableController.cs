using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.Admin.Repositories.Interfaces;

namespace Project.Areas.Admin.Controllers.Table
{
    [Authorize(Roles = "Admin")]
    public class SemesterTable : Controller
    {
        private readonly ISemesterRepository _semesterRepository;

        public SemesterTable(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public ActionResult Index(int disciplineId)
        {
            var semesters = _semesterRepository
                .GetAll()
                .Where(s => s.DisciplineId == disciplineId) // Фильтруем семестры по дисциплине
                .ToList();

            return View(semesters);
        }
    }
}
