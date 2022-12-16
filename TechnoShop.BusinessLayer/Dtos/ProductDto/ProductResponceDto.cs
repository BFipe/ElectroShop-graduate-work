using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.BusinessLayer.Dtos.ProductDto
{
    public class ProductResponceDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public int Count { get; set; }
        public int InOrderCount { get; set; }
        public string Description { get; set; }
        public string ProductTypeName { get; set; }
        public int ProductRate { get; set; }
        public bool IsOpenForCart { get; set; } = true;
    }
}
