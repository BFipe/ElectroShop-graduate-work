using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Entities.ProductEntity
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; } 
        public int ProductPage { get; set; }

        public string ProductTypeName { get; set; }
        public ProductType ProductType { get; set; }
    }
}
