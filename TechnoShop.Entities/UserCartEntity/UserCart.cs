using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Entities.CartEntity
{
    public class UserCart
    {
        public string TechnoShopUserId { get; set; }
        public TechnoShopUser TechnoShopUser { get; set; }
        
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductCount { get; set; }   
    }
}
