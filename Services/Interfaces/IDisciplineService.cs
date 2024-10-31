using OfficeOpenXml;
using Project.Models.Parser;

namespace Project.Services.Interfaces
{
    public interface IDisciplineService
    {
        List<Discipline> ProcessDisciplines(ExcelWorksheet worksheet, int startRow);
        void ClearAll();
    }
}