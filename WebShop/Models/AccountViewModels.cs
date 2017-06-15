using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле email является обязательным!")]
        [RegularExpression(@"([^а-яА-Я]*)", ErrorMessage = "Некорректный email")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        [StringLength(100, ErrorMessage = "Поле e-mail не должно превышать 100 символов.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле Имя является обязательным!.")]
        [StringLength(150, ErrorMessage = "Поле Имя не должно превышать 150 символов.")]
        [RegularExpression(@"([A-Za-zА-Яа-я-]*)", ErrorMessage = "Имя может содержать только латинские и русские буквы и знак '-'")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Фамилия является обязательным!")]
        [RegularExpression(@"([A-Za-zА-Яа-я-]*)", ErrorMessage = "Фамилия может содержать только латинские и русские буквы и знак '-'")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле Страна является обязательным!")]
        public string Country { get; set; }

        [StringLength(50, ErrorMessage = "Поле Город не должно превышать 50 символов.")]
        public string City { get; set; }
        [StringLength(13, ErrorMessage = "Поле Телефон не должно превышать 13 символов.")]
        [RegularExpression("([+]?[0-9]{1,12})", ErrorMessage = "Неверный формат поля Телефон")]
        public string Phone { get; set; }
        [Required]
        public long Cl_status_id { get; set; }
        [Required(ErrorMessage = "Поле Пароль является обязательным")]
        [RegularExpression(@"([A-Za-z0-9!%?*_]*)", ErrorMessage = "Некорректный пароль")]
        [StringLength(15, ErrorMessage = "Пароль должен иметь длину от 6 до 16 символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль должен содержать минимум 6 символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
