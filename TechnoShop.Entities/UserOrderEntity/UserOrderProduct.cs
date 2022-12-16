using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Entities.UserOrderEntity
{
    public class UserOrderProduct
    {
        public string UserOrderId { get; set; }
        public UserOrder UserOrder { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductCount { get; set; }
    }
}
