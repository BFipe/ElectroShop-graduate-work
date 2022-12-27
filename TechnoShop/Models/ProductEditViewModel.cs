using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechnoShop.Models
{
    public class ProductEditViewModel
    {
        public string Id { get; set; }

        public int ProductRate { get; set; }

        [Display(Name = "Название продукта")]
        public string Name { get; set; }

        [Display(Name = "Стоимость")]
        public double Cost { get; set; }

        [Display(Name = "Кол-во продукта")]
        public int Count { get; set; }
        
        [Display(Name = "Минимальное кол-во продукта")]
        public int MinCount { get; set; }

        [Display(Name = "Описание продукта")]
        public string Description { get; set; }

        [Display(Name = "Тип продукта")]
        public string ProductTypeName { get; set; }

        [Display(Name = "Ссылка на фотографию продукта")]
        public string PictureLink { get; set; }

        public ResponceStatusViewModel ResponceStatus { get; set; } = new();
    }
}
