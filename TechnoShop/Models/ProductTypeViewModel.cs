using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechnoShop.Models
{
    public class ProductTypeViewModel
    {
        [Required(ErrorMessage = "Необходимо добавить название типа продукта!")]
        [MaxLength(60, ErrorMessage = "Максимальная длинна названия типа - 60")]
        [Display(Name = "Название типа продукта")]
        public string TypeName { get; set; }

        public List<string> ErrorListInfo { get; set; } = new();
        public List<string> StatusListInfo { get; set; } = new();
    }
}
