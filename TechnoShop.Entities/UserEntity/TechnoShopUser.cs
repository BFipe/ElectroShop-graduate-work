using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.CartEntity;
using TechnoShop.Entities.ProductEntity;

namespace TechnoShop.Entities.UserEntity
{
    public class TechnoShopUser : IdentityUser
    {
        public List<Product> Products { get; set; } = new();
        public List<UserCart> UserCarts { get; set; } = new();
    }
}
