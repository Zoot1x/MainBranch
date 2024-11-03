using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.Admin.Repositories.Interfaces;

namespace Project.Areas.Admin.Controllers.Table
{
    [AllowAnonymous]
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