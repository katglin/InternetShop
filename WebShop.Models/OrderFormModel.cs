using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class OrderFormModel
    {
        [Required(ErrorMessage = "Поле Фамилия является обязательным")]
        [RegularExpression("([a-zA-ZА-Яа-яёЁ]*)", ErrorMessage = "Поле может содержать только буквы")]
        [StringLength(100, ErrorMessage = "Поле Фамилия должно содержать не более 100 символов")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле Имя является обязательным")]
        [RegularExpression("([a-zA-ZА-Яа-яёЁ]*)", ErrorMessage = "Поле может содержать только буквы")]
        [StringLength(100, ErrorMessage = "Поле Имя должно содержать не более 100 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Адрес является обязательным")]
        [StringLength(150, ErrorMessage = "Поле Адрес должно содержать не более 100 символов")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Поле Email должно быть заполнено")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Некорректное значение поля Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Телефон является обязательным")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([+]?[0-9]{1,12})", ErrorMessage = "Поле телефон должно содержать только цифры и, возможно, знак +")]
        [StringLength(13, ErrorMessage = "Телефонный номер должен содержать 13 символов."),
            MinLength(3, ErrorMessage = "Телефонный номер должен содержать хотя бы 3 символа")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
       
        [Display(Name = "К оплате")]
        public decimal OrderSum { get; set; }
        public List<Int64> Cart_ids { get; set; }
    }
}
