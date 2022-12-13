using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Data.Repositories.Interfaces;
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
            return _dbContext.TechnoShopUsers.Include(q => q.Products).SingleOrDefaultAsync(q => q.Email == email);
        }
    }
}
