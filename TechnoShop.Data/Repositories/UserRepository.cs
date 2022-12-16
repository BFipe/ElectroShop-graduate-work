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

namespace TechnoShop.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public Task<TechnoShopUser> FindUserByEmail(string email)
        {
            return _dbContext.TechnoShopUsers.Include(q => q.Products).Include(q => q.UserOrders).SingleOrDefaultAsync(q => q.Email == email);
        }

        public void AddProductToCart(TechnoShopUser user, Product product, int cartCount)
        {
            user.UserCarts.Add(new UserCart { Product = product, ProductCount = cartCount });
        }
    }
}
