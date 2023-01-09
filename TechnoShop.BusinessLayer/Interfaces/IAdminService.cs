using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.AdminDtos;

namespace TechnoShop.BusinessLayer.Interfaces
{
    public interface IAdminService
    {
        public Task<List<TechnoShopUserDto>> AllUsers();
        public Task<List<IdentityRole>> AllRoles();
        public Task<IdentityResult> AddRole(string roleName);
        public Task<IdentityResult> DeleteRole(string roleId);
    }
}
