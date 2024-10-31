using Microsoft.AspNetCore.Mvc;
using Project.Repositories.Interfaces;

namespace Project.Controllers.Table
{
    [Route("[controller]")]
    public class DisciplineTable : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;

        public DisciplineTable(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        public ActionResult Index(int specialityId)
        {
            var disciplines = _disciplineRepository
                .GetAll()
                .Where(d => d.SpecialityId == specialityId)
                .ToList();

            // string disciplineTitle = _disciplineRepository.

            return View(disciplines);
        }
    }
}
