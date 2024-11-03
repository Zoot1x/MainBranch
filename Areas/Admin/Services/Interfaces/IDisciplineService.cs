using OfficeOpenXml;
using Project.Areas.Admin.Models.Parser;

namespace Project.Areas.Admin.Services.Interfaces
{
    public interface IDisciplineService
    {
        List<Discipline> ProcessDisciplines(ExcelWorksheet worksheet, int startRow);
    }
}