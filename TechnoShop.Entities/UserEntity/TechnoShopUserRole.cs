using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Entities.UserEntity
{
    public class TechnoShopUserRole : IdentityUserRole<string>
    {
        public virtual string UserId { get; set; }
        public TechnoShopUser User { get; set; }

        public virtual string RoleId { get; set; }
        public TechnoShopRole Role { get; set; }
    }
}
