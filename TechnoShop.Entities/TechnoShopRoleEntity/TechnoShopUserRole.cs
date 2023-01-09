using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Entities.UserRoleEntity
{
    public class TechnoShopUserRole
    {
        public string UserId { get; set; }
        public TechnoShopUser User { get; set; }

        public string RoleId { get; set; }
        public TechnoShopRole Role { get; set; }
    }
}
