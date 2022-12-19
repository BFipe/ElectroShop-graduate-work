using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;
using TechnoShop.Entities.UserOrderEntity;

namespace TechnoShop.Data.Repositories.Interfaces
{
    public interface ICartRepository
    {
        public void AddProductToCart(TechnoShopUser user, Product product, int cartCount);
        public Task AddNewOrder(UserOrder userOrder);
    }
}
