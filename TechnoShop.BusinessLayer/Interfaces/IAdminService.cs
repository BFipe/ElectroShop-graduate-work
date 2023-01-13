using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.AdminDtos;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.BusinessLayer.Interfaces
{
    public interface IAdminService
    {
        public Task<List<TechnoShopUserDto>> AllUsers();
        public Task<List<TechnoShopRole>> AllRoles();
        public Task<IdentityResult> AddRole(string roleName);
        public Task<IdentityResult> DeleteRole(string roleId);
        public Task<IdentityResult> AddRoleToUser(string userId, string roleName);
        public Task<IdentityResult> DeleteRoleFromUser(string userId, string roleName);
        public Task<IdentityResult> ConfirmEmail(string userId);
    }
}
