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

        public void UploadExcel(IFormFile file)
        {
            var specializations = new List<Speciality>();
            var year = 2025;
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
                            var disciplines = _disciplineService.ProcessDisciplines(worksheet, 10);
                            var specialization = new Speciality
                            {
                                Title = specialityTitle,
                                Disciplines = disciplines,
                                KursNumber = kusrnumber,
                            };

                            specializations.Add(specialization);
                        }
                    }

                    _specialityRepository.AddRange(specializations);
                    _specialityRepository.SaveChanges();
                }
            }
        }
    }
}
