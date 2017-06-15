using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace WebShop.Models
{
    public class IndexViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле Имя является обязательным")]
        [RegularExpression("([a-zA-ZА-Яа-яёЁ]*)", ErrorMessage = "Имя может содержать только буквы")]
        [StringLength(100, ErrorMessage = "Поле Имя должно содержать не более 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Фамилия является обязательным")]
        [RegularExpression("([a-zA-ZА-Яа-яёЁ]*)", ErrorMessage = "Фамилия может содержать только буквы")]
        [StringLength(100, ErrorMessage = "Поле Фамилия должно содержать не более 100 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле Телефон является обязательным")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([+]?[0-9]{1,12})", ErrorMessage = "Телефонный номер может содержать до 12 цифр и, возможно, знак +")]
        [StringLength(13, ErrorMessage = "Телефонный номер должен содержать 13 символов."),
            MinLength(1, ErrorMessage = "Телефонный номер должен содержать хотя бы 3 символа")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле Страна является обязательным")]
        [RegularExpression("([a-zA-ZА-Яа-яёЁ]*)", ErrorMessage = "Страна может содержать только буквы")]
        [StringLength(50, ErrorMessage = "Поле Страна должно содержать не более 50 символов")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Поле Город является обязательным")]
        [RegularExpression("([a-zA-ZА-Яа-яёЁ]*)", ErrorMessage = "Город может содержать только буквы")]
        [StringLength(50, ErrorMessage = "Название города не должно превышать 50 символов.")]
        public string City { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "Поле {0} должно содержать от {2} до 15 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Введите текущий пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

    //    [RegularExpression(@"([A-Za-z0-9!%?*_]*)", ErrorMessage = "Пароль может содержать только латинские буквы, цыфры и символы ! % ? * _")]
        [Required(ErrorMessage = "Введите новый пароль")]
        [StringLength(15, ErrorMessage = "Поле {0} должно содержать от {2} до 15 символов", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Пароль недостаточно надежен")]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

       // [RegularExpression(@"([A-Za-z0-9!%?*_]*)", ErrorMessage = "Пароль может содержать только латинские буквы, цыфры и символы ! % ? * _")]
        [Required(ErrorMessage = "Повторите новый пароль")]
        [DataType(DataType.Password, ErrorMessage = "Пароль недостаточно надежен")]
        [Display(Name = "Повторите пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}