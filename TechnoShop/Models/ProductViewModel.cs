using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TechnoShop.Entities.Enums;

namespace TechnoShop.Models
{
    public class ProductViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [DefaultValue(0)]
        public double Cost { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Count { get; set; }

        [Required]
        [MaxLength(600)]
        public string Description { get; set; }

        [Required]
        public ProductType ProductType { get; set; }
    }
}
