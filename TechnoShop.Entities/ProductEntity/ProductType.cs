using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Entities.ProductEntity
{
    public class ProductType
    {
        public string ProductTypeId { get; set; }
        public string TypeName { get; set; }

        public List<Product> Products { get; set; }
    }
}
