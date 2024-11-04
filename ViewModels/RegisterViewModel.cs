using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Логин должен содержать от 5 до 20 символов.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Логин может содержать только буквы.")]
        public string?  UserName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string?  Name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }
        [Required]
        [Display(Name = "Отчетство")]
        public string? FatherName {get; set;}

        [Required]
        [Display(Name = "Номер группы")]
        public int? GroupNumber { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Неправильный пароль")]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string? PasswordConfirm { get; set; }
    }
}
