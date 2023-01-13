using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Entities.CartEntity;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;
using TechnoShop.Exceptions;

namespace TechnoShop.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<TechnoShopUser>> ReturnAllUsersWithRolesAsync()
        {
            return _dbContext.TechnoShopUsers
                .Include(q => q.TechnoShopRoles).ToListAsync();
        }

        public Task<TechnoShopUser> FindUserByEmail(string email)
        {
            return _dbContext.TechnoShopUsers
                .Include(q => q.Products)
                .Include(q => q.UserOrders).ThenInclude(q => q.UserOrderProducts).ThenInclude(q => q.Product)
                .Include(q => q.TechnoShopRoles)
                .SingleOrDefaultAsync(q => q.Email == email);
        }

        public async Task<IdentityResult> AddRoleToUser(string userId, string roleName)
        {
            var user = await _dbContext.Users.Include(q => q.TechnoShopRoles).SingleOrDefaultAsync(q => q.Id == userId);
            if (user == null) return IdentityResult.Failed(new IdentityError() { Code = "0", Description = $"User with Id {userId} not found" });

            var role = await _dbContext.TechnoShopRoles.SingleOrDefaultAsync(q => q.Name == roleName);
            if (role == null) return IdentityResult.Failed(new IdentityError() { Code = "0", Description = $"Role {roleName} not found" });

            if (user.TechnoShopRoles.Any(q => q == role)) return IdentityResult.Failed(new IdentityError() { Code = "0", Description = $"User with Id {userId} already have role {roleName}" });

            try
            {
                user.TechnoShopRoles.Add(role);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "0", Description = ex.Message });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> RemoveRoleFromUser(string userId, string roleName)
        {
            var user = await _dbContext.Users.Include(q => q.TechnoShopRoles).SingleOrDefaultAsync(q => q.Id == userId);
            if (user == null) return IdentityResult.Failed(new IdentityError() { Code = "0", Description = $"User with Id {userId} not found" });

            var role = await _dbContext.TechnoShopRoles.SingleOrDefaultAsync(q => q.Name == roleName);
            if (role == null) return IdentityResult.Failed(new IdentityError() { Code = "0", Description = $"Role {roleName} not found" });

            if (user.TechnoShopRoles.Any(q => q == role) == false) return IdentityResult.Failed(new IdentityError() { Code = "0", Description = $"User with Id {userId} does not have role {roleName}" });

            try
            {
                user.TechnoShopRoles.Remove(role);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "0", Description = ex.Message });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ConfirmEmail(string userId)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(q => q.Id == userId);
            if (user == null) return IdentityResult.Failed(new IdentityError() { Code = "0", Description = $"User with Id {userId} not found" });
            if (user.EmailConfirmed == true) return IdentityResult.Failed(new IdentityError() { Code = "0", Description = $"User already has confirmed email" });

            try
            { 
                user.EmailConfirmed = true;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "0", Description = ex.Message });
            }

            return IdentityResult.Success;
        }
    }
}
