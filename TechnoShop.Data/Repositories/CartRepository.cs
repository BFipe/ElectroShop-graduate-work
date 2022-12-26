using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Entities.CartEntity;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;
using TechnoShop.Entities.UserOrderEntity;

namespace TechnoShop.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CartRepository(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public void AddProductToCart(TechnoShopUser user, Product product, int cartCount)
        {
            user.UserCarts.Add(new UserCart { Product = product, ProductCount = cartCount });
        }

        public Task AddNewOrder(UserOrder userOrder)
        {
            return _dbContext.UserOrders.AddAsync(userOrder).AsTask();
        }

        public Task<int> OrdersCount()
        {
            return _dbContext.UserOrders.CountAsync();
        }
    }
}
