using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TechnoShop.Models
{
    public class UserPurchaseDataViewModel
    {
        [Required(ErrorMessage = "Необходимо заполнить поле с названием!")]
        [MaxLength(150, ErrorMessage = "Максимальная длинна строки - 150")]
        [Display(Name = "Название продукта")]
        public string Name { get; set; }
    }
}
