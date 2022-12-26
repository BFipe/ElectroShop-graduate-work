using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TechnoShop.Models
{
    public class UserOrderDataViewModel
    {
        [Required(ErrorMessage = "Необходимо заполнить поле с именем!")]
        [MaxLength(60, ErrorMessage = "Максимальная длинна строки - 60")]
        [Display(Name = "Ваше полное имя")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле с номером телефона!")]
        [Display(Name = "Ваш номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле с названием города!")]
        [MaxLength(30, ErrorMessage = "Максимальная длинна строки - 30")]
        [Display(Name = "Ваш город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле с названием улицы!")]
        [MaxLength(40, ErrorMessage = "Максимальная длинна строки - 40")]
        [Display(Name = "Ваша улица")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле с номером дома!")]
        [MaxLength(30, ErrorMessage = "Максимальная длинна строки - 30")]
        [Display(Name = "Ваш номер дома")]
        public string HouseNumber { get; set; }

        [MaxLength(30, ErrorMessage = "Максимальная длинна строки - 30")]
        [Display(Name = "Ваш номер квартиры")]
        public string FlatNumber { get; set; }

        [MaxLength(30, ErrorMessage = "Максимальная длинна строки - 30")]
        [Display(Name = "Ваш номер подъезда")]
        public string Entrance { get; set; }

        [MaxLength(30, ErrorMessage = "Максимальная длинна строки - 30")]
        [Display(Name = "Ваш этаж")]
        public string Floor { get; set; }

        [MaxLength(30, ErrorMessage = "Максимальная длинна строки - 150")]
        [Display(Name = "Комментарий к заказу")]
        public string OrderComment { get; set; }

        [Display(Name = "Опция для отправки заказа на почту")]
        public bool SendEmail { get; set; }
    }
}
