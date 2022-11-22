using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.Enums;

namespace TechnoShop.BusinessLayer.Dtos.ProductDto
{
    public class ProductResponceDTo
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        
    }
}
