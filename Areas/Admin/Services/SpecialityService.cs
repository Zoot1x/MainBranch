using OfficeOpenXml;
using Project.Areas.Admin.Models.Parser;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Areas.Admin.Services.Interfaces;

namespace Project.Areas.Admin.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IDisciplineService _disciplineService;

        public SpecialityService(
            ISpecialityRepository specialityRepository,
            IDisciplineService disciplineService
        )
        {
            _specialityRepository = specialityRepository;
            _disciplineService = disciplineService;
        }

        public void ClearAll()
        {
            var specialities = _specialityRepository.GetAll().ToList();
            _specialityRepository.RemoveRange(specialities);
            _specialityRepository.SaveChanges();
        }

        public bool UploadExcel(IFormFile file)
        {
            var specializations = new List<Speciality>();
            var year = DateTime.Now.Year + 1;
            bool isNewSpecialityAdded = false;
            if (file == null || file.Length == 0 || !file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                // Возвращаем false для обозначения ошибки расширения файла
                return false;
            }
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string[] fileNameParts = fileName.Split(' ');
                    string CodeName = fileNameParts.Length > 0 ? fileNameParts[0] : string.Empty;
                    string Year =
                        fileName.Length > 4 ? fileName.Substring(fileName.Length - 4) : fileName;

                    for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                    {
                        if ((i + 1) % 2 == 0)
                        {
                            var worksheet = package.Workbook.Worksheets[i];
                            var specialityTitle = $"{worksheet.Name} ({CodeName}, {Year})";
                            var kusrnumber = year - int.Parse(Year);

                            if (_specialityRepository.GetAll().Any(s => s.Title == specialityTitle))
                            {
                                // Если такая специальность уже существует, пропускаем добавление
                                return false;
                            }

                            var disciplines = _disciplineService.ProcessDisciplines(worksheet, 10);
                            var specialization = new Speciality
                            {
                                Title = specialityTitle,
                                Disciplines = disciplines,
                                KursNumber = kusrnumber,
                            };

                            specializations.Add(specialization);
                            isNewSpecialityAdded = true;
                        }
                    }

                    if (specializations.Any())
                    {
                        _specialityRepository.AddRange(specializations);
                        _specialityRepository.SaveChanges();
                    }
                }
                return isNewSpecialityAdded;
            }
        }
    }
}
