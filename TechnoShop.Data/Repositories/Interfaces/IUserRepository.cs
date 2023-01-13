using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<TechnoShopUser> FindUserByEmail(string email);
        public Task<IdentityResult> AddRoleToUser(string userId, string roleName);
        public Task<IdentityResult> RemoveRoleFromUser(string userId, string roleName);
        public Task<IdentityResult> ConfirmEmail(string userId);
        public Task<List<TechnoShopUser>> ReturnAllUsersWithRolesAsync();
    }
}
