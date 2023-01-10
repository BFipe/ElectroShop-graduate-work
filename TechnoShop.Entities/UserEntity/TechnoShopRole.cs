using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Entities.UserEntity
{
    public class TechnoShopRole : IdentityRole
    {
        public TechnoShopRole() : base() { }

        public TechnoShopRole(string roleName) : base(roleName)
        {

        }

        public List<TechnoShopUser> TechnoShopUsers { get; set; } = new();

        public List<TechnoShopUserRole> TechnoShopUserRoles { get; set; } = new();
    }
}
