using Microsoft.AspNetCore.Mvc;
using Project.Repositories.Interfaces;

namespace Project.Controllers.Table
{
    [Route("[controller]")]
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
