using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.CartEntity;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserOrderEntity;
using TechnoShop.Entities.UserRoleEntity;

namespace TechnoShop.Entities.UserEntity
{
    public class TechnoShopUser : IdentityUser
    {
        public List<Product> Products { get; set; } = new();

        public List<UserCart> UserCarts { get; set; } = new();

        public List<UserOrder> UserOrders { get; set; } = new();

        public List<TechnoShopRole> TechnoShopRoles { get; set; } = new();
    }
}
