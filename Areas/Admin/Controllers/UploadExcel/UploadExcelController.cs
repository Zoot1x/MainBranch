using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.Admin.Services.Interfaces;

namespace Project.Areas.Admin.Controllers.UploadExcel
{
    [Authorize(Roles = "Admin")]
    public class UploadExcel : Controller
    {
        private readonly ISpecialityService _specialityService;
        private readonly IDisciplineService _disciplineService;

        public UploadExcel(
            ISpecialityService specialityService,
            IDisciplineService disciplineService
        )
        {
            _specialityService = specialityService;
            _disciplineService = disciplineService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClearTable()
        {
            _specialityService.ClearAll();
            TempData["ClearAccess"] = "Успешно очищенно";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["FileError"] = "Вы не выбрали файл";
                return RedirectToAction("Index");
            }

            // Проверка расширения файла
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["FileError"] = "Файл должен иметь расширение .xlsx.";
                return RedirectToAction("Index");
            }

            bool isUploaded = _specialityService.UploadExcel(file);

            if (!isUploaded)
            {
                TempData["SpecialityExists"] = "Такой файл уже существует.";
            }
            else
            {
                TempData["UploadSuccess"] = "Файл успешно загружен.";
            }

            return RedirectToAction("Index");
        }
    }
}
