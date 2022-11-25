using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TechnoShop.Entities.Enums;

namespace TechnoShop.Models
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Необходимо заполнить поле с названием!")]
        [MaxLength(150, ErrorMessage = "Максимальная длинна строки - 150")]
        [Display(Name = "Название продукта")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле цены!")]
        [DefaultValue(0)]
        [Display(Name = "Стоимость")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле кол-ва продуктов!")]
        [DefaultValue(1)]
        [Display(Name = "Кол-во продукта")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле описания!")]
        [MaxLength(600, ErrorMessage = "Максимальная длинна строки - 600")]
        [Display(Name = "Описание продукта")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать значение типа продукта!")]
        [Display(Name = "Тип продукта")]
        public ProductType ProductType { get; set; }

        public List<string> ErrorListInfo { get; set; } = new();
        public List<string> StatusListInfo { get; set; } = new();
    }
}
