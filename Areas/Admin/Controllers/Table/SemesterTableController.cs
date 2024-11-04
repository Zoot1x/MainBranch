using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Areas.Admin.ViewModels;

namespace Project.Areas.Admin.Controllers.Table
{
    [Authorize(Roles = "Admin")]
    public class SemesterTable : Controller
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly IDisciplineRepository _disciplineRepository;

        public SemesterTable(ISemesterRepository semesterRepository, IDisciplineRepository disciplineRepository)
        {
            _semesterRepository = semesterRepository;
            _disciplineRepository = disciplineRepository;
        }

        public ActionResult Index(int disciplineId)
        {
            var disciplines = _disciplineRepository.GetAll()
                .FirstOrDefault(s => s.Id == disciplineId);

            var semesters = _semesterRepository
                .GetAll()
                .Where(s => s.DisciplineId == disciplineId) // Фильтруем семестры по дисциплине
                .ToList();

            var semestertableviewmodel = new SemesterTableViewModel
            {
                Semesters = semesters,
                DisciplineTitle = disciplines.Title
            };

            return View(semestertableviewmodel);
        }
    }
}
