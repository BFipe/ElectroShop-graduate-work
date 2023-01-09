using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.UserRoleEntity;

namespace TechnoShop.Models.AdminViewModels
{
    public class CombinedAllRolesViewModel
    {
        public ResponceStatusViewModel Responce { get; set; }

        public List<TechnoShopRole> Roles { get; set; }
    }
}
