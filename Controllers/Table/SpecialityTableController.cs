using Microsoft.AspNetCore.Mvc;
using Project.Repositories;

namespace Project.Controllers.Table
{
    [Route("[controller]")]
    public class SpecialityTable : Controller
    {
        private readonly ISpecialityRepository _specialityRepository;

        public SpecialityTable (ISpecialityRepository specialityRepository)
        {
            _specialityRepository = specialityRepository;
        }
        public IActionResult Index()
        {
           var specialities = _specialityRepository.GetAll();
            return View(specialities);
        }
    }
}