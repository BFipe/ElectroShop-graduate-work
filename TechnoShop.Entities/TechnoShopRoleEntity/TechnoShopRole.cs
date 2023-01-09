using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Entities.UserRoleEntity
{
    public class TechnoShopRole : IdentityRole
    {
        public TechnoShopRole(string roleName) : base(roleName)
        {
        }
        
        public TechnoShopRole() : base()
        {
        }

        public List<TechnoShopUser> TechnoShopUsers { get; set; } = new();

        public List<TechnoShopUserRole> TechnoShopUserRoles { get; set; } = new();
    }
}
