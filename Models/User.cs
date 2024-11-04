using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public string? Password { get; set; }
        public int? GroupNumber { get; set; }
        public int? KusrNumber => CalculateKurs();
        public Roles Role { get; set; }

        public int? CurrentSemester => CalculateCurrentSemester();

        private int? CalculateCurrentSemester()
        {
            if (GroupNumber.HasValue)
            {
                // Извлекаем номер курса (вторая цифра)
                int course = (GroupNumber.Value / 10) % 10;
                DateTime currentDate = DateTime.Now;

                // Определяем, какой семестр сейчас
                int semesterNumber = (course - 1) * 2; // Базовый номер семестра для курса

                // Проверяем дату, чтобы понять, осенний (1 сентября) или весенний (12 января) семестр
                if (currentDate.Month > 8 || (currentDate.Month == 8 && currentDate.Day >= 1))
                {
                    semesterNumber += 1; // Осенний семестр (с 1 сентября до 11 января)
                }
                else
                {
                    semesterNumber += 2; // Весенний семестр (с 12 января до 31 августа)
                }

                // Проверяем, не превышает ли семестр 10
                if (semesterNumber > 10)
                {
                    return null; // Если семестр больше 10, значит обучение завершено
                }

                return semesterNumber;
            }
            return null; // Если номер группы недействительный
        }

        private int? CalculateKurs()
        {
            if (GroupNumber.HasValue)
            {
                // Извлекаем номер курса (вторая цифра)
                int course = (GroupNumber.Value / 10) % 10;
                return course;
            }
            return null; // Если номер группы недействительный
        }
    }

    public enum Roles
    {
        [Display(Name = "Курсант")]
        Cadet,

        [Display(Name = "Преподаватель")]
        Teacher,

        [Display(Name = "Модератор")]
        Moderator,

        [Display(Name = "Администратор")]
        Admin,
    }
}
