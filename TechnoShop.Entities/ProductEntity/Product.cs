using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.CartEntity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Entities.ProductEntity
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; } 
        public int ProductRate { get; set; }

        public string ProductTypeName { get; set; }
        public ProductType ProductType { get; set; }

        public List<TechnoShopUser> TechnoShopUsers { get; set; } = new();
        public List<UserCart> UserCarts { get; set; } = new();
    }
}
