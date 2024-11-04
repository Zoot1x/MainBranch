using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Areas.Admin.ViewModels;

namespace Project.Areas.Admin.Controllers.Table
{
    [Authorize(Roles = "Admin")]
    public class DisciplineTable : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly ISpecialityRepository _specialityRepository;

        public DisciplineTable(IDisciplineRepository disciplineRepository, ISpecialityRepository specialityRepository)
        {
            _disciplineRepository = disciplineRepository;
            _specialityRepository = specialityRepository;
        }

        public ActionResult Index(int specialityId)
        {
            // Получаем специальность по идентификатору
            var speciality = _specialityRepository.GetAll()
                .FirstOrDefault(s => s.Id == specialityId);

            // Если специальность не найдена, можно вернуть ошибку или перенаправить на другую страницу
            if (speciality == null)
            {
                // Обработка случая, когда специальность не найдена
                return NotFound(); // или RedirectToAction("Index", "AnotherController");
            }

            
            var disciplines = _disciplineRepository
                .GetAll()
                .Where(d => d.SpecialityId == specialityId)
                .ToList();

            var disciplinetableviewmodel = new DisciplineTableViewModel 
            {
                Disciplines = disciplines,
                SpecialityTitle = speciality.Title
            };

            return View(disciplinetableviewmodel);
        }
    }
}
