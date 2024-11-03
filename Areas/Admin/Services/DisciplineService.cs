using OfficeOpenXml;
using Project.Areas.Admin.Models.Parser;
using Project.Areas.Admin.Services.Interfaces;

namespace Project.Areas.Admin.Services
{
    public class DisciplineService : IDisciplineService
    {
        public List<Discipline> ProcessDisciplines(ExcelWorksheet worksheet, int startRow)
        {
            var disciplines = new List<Discipline>();

            //ДИСЦИЛИНЫ БЕЗ АТТЕСТАЦИИ В СЕМЕСТРАХ
            List<string> DisciplineWithoutScore = new List<string>
            {
                "Управление подразделениями в мирное время",
                "Общевоинские уставы Вооруженных Сил Российской Федерации",
                "Строевая подготовка",
            };

            for (int row = startRow; row <= worksheet.Dimension.End.Row; row++)
            {
                
                var disciplineTitle = worksheet.Cells[row, 3].Text;
                if (disciplineTitle == "Физическая подготовка*")
                    disciplineTitle = "Физическая подготовка";

                var exam = worksheet.Cells[row, 235].Text;
                var score_mark = worksheet.Cells[row, 236].Text;
                var score_no_mark = worksheet.Cells[row, 237].Text;
                var coursework = worksheet.Cells[row, 238].Text;

                bool hasValue =
                    !string.IsNullOrWhiteSpace(exam)
                    || !string.IsNullOrWhiteSpace(score_mark)
                    || !string.IsNullOrWhiteSpace(score_no_mark)
                    || !string.IsNullOrWhiteSpace(coursework)
                    || DisciplineWithoutScore.Contains(disciplineTitle);

                if (hasValue && !string.IsNullOrWhiteSpace(disciplineTitle))
                {
                    var semesters = CalculateSemesters(worksheet, row);
                    var discipline = new Discipline
                    {
                        Title = disciplineTitle,
                        Exam = exam,
                        Score_mark = score_mark,
                        Score_no_mark = score_no_mark,
                        Coursework = coursework,
                        Semesters = semesters,
                    };

                    disciplines.Add(discipline);
                }
            }

            return disciplines;
        }

        private List<Semester> CalculateSemesters(ExcelWorksheet worksheet, int row)
        {
            var semesters = new List<Semester>();

            for (int sem = 1; sem <= 10; sem++)
            {
                int studyHours = 0,
                    selfStudyHours = 0;

                switch (sem)
                {
                    case 1:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 25].Text)
                            && worksheet.Cells[row, 25].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 25]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 45]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 2:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 46].Text)
                            && worksheet.Cells[row, 46].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 46]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 66]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 3:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 67].Text)
                            && worksheet.Cells[row, 67].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 67]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 87]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 4:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 88].Text)
                            && worksheet.Cells[row, 88].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 88]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 108]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 5:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 109].Text)
                            && worksheet.Cells[row, 109].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 109]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 129]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 6:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 130].Text)
                            && worksheet.Cells[row, 130].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 130]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 150]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 7:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 151].Text)
                            && worksheet.Cells[row, 151].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 151]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 171]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 8:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 172].Text)
                            && worksheet.Cells[row, 172].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 172]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 192]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 9:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 193].Text)
                            && worksheet.Cells[row, 193].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 193]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 213]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;

                    case 10:
                        if (
                            !string.IsNullOrEmpty(worksheet.Cells[row, 214].Text)
                            && worksheet.Cells[row, 214].Value.ToString() != "0"
                        )
                        {
                            studyHours = int.Parse(
                                worksheet.Cells[row, 214]?.Value?.ToString() ?? "0"
                            );
                            selfStudyHours = int.Parse(
                                worksheet.Cells[row, 234]?.Value?.ToString() ?? "0"
                            );
                        }
                        break;
                }
                if (studyHours > 0 || selfStudyHours > 0)
                {
                    semesters.Add(
                        new Semester
                        {
                            Number = sem,
                            StudyHours = studyHours,
                            SelfStudyHours = selfStudyHours,
                        }
                    );
                }
            }

            return semesters;
        }
    }
}
