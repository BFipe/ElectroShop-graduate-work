using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechnoShop.Models
{
    public class ProductResponceViewModel
    {
        public string Id { get; set; }

        public int ProductRate { get; set; }

        public bool IsOpenForCart { get; set; }

        [Display(Name = "Название продукта")]
        public string Name { get; set; }

        [Display(Name = "Стоимость")]
        public double Cost { get; set; }

        [Display(Name = "Кол-во продукта")]
        public int Count { get; set; }

        [Display(Name = "Описание продукта")]
        public string Description { get; set; }

        [Display(Name = "Тип продукта")]
        public string ProductTypeName { get; set; }

        [Display(Name = "Ссылка на фотографию продукта")]
        public string PictureLink { get; set; }

        public List<string> ErrorListInfo { get; set; } = new();
        public List<string> StatusListInfo { get; set; } = new();
    }
}
