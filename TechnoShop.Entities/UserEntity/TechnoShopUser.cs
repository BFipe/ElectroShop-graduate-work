using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.CartEntity;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserOrderEntity;

namespace TechnoShop.Entities.UserEntity
{
    public class TechnoShopUser : IdentityUser
    {
        public virtual List<IdentityUserClaim<string>> Claims { get; set; }

        public virtual List<IdentityUserLogin<string>> Logins { get; set; }

        public virtual List<IdentityUserToken<string>> Tokens { get; set; }

        public List<Product> Products { get; set; } = new();

        public List<UserCart> UserCarts { get; set; } = new();

        public List<UserOrder> UserOrders { get; set; } = new();

        public List<TechnoShopRole> TechnoShopRoles { get; set; } = new();

        public List<TechnoShopUserRole> TechnoShopUserRoles { get; set; } = new();
    }
}
